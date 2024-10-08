using Hangfire;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.DTOs.ProductsDTOs.CampaignDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductDTOs;
using RestaurantApp.Application.Services.ProductsServices.CampaignServices;
using RestaurantApp.Domain.Contracts.ProductContracts.CampaignRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductCampaignRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Enums;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductCampaignRepository _productCampaignRepository;

    public CampaignService(ICampaignRepository campaignRepository, IProductRepository productRepository, IProductCampaignRepository productCampaignRepository)
    {
        _campaignRepository = campaignRepository;
        _productRepository = productRepository;
        _productCampaignRepository = productCampaignRepository;
    }

    public async Task<IDataResult<CampaignDTO>> AddAsync(CampaignCreateDTO campaignCreateDTO)
    {
        if (await _campaignRepository.AnyAsync(x => x.Name.ToLower() == campaignCreateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<CampaignDTO>("Girilen kampanya sistemde mevcut!");
        }

        var newCampaign = campaignCreateDTO.Adapt<Campaign>();
        newCampaign.ProductCampaigns = campaignCreateDTO.ProductIds.Select(productId => new ProductCampaign
        {
            CampaignId = newCampaign.Id,
            ProductId = productId
        }).ToList();

        DataResult<CampaignDTO> result = new ErrorDataResult<CampaignDTO>("Kampanya Ekleme Başarısız!");

        var strategy = await _campaignRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _campaignRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                await _campaignRepository.AddAsync(newCampaign);
                await _campaignRepository.SaveChangesAsync();

                // Kampanya başlangıç ve bitiş
                BackgroundJob.Schedule(() => ActivateCampaign(newCampaign.Id), newCampaign.StartDate);
                BackgroundJob.Schedule(() => DeactivateCampaign(newCampaign.Id), newCampaign.EndDate);

                result = new SuccessDataResult<CampaignDTO>(newCampaign.Adapt<CampaignDTO>(), "Kampanya Ekleme Başarılı!");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<CampaignDTO>("Kampanya Ekleme Başarısız: " + ex.Message);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });

        return result;
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var campaign = await _campaignRepository.GetByIdAsync(id);
        if (campaign == null)
        {
            return new ErrorResult("Kampanya bulunamadı");
        }
        try
        {
            await _campaignRepository.DeleteAsync(campaign);
            await _campaignRepository.SaveChangesAsync();
            return new SuccessResult("Kampanya silme işlemi başarılı");
        }
        catch (Exception)
        {
            return new ErrorResult("Kampanya silme işlemi sırasında bir hata oluştu");
        }
    }

    public async Task<IDataResult<List<CampaignListDTO>>> GetAllAsync()
    {
        var campaigns = await _campaignRepository.GetAllAsync();
        if (campaigns.Count() <= 0)
        {
            return new ErrorDataResult<List<CampaignListDTO>>(campaigns.Adapt<List<CampaignListDTO>>(), "Listelenecek kampanya bulunamadı!");
        }

        var campaignListDTOs = campaigns.Adapt<List<CampaignListDTO>>();

        // İndirimli fiyatları hesaplayarak ekleyelim
        foreach (var campaign in campaignListDTOs)
        {
            var products = await _productRepository.GetAllAsync(p => p.ProductCampaigns.Any(pc => pc.CampaignId == campaign.Id));
            campaign.OriginalPrices = products.Select(p => p.Price).ToList();
        }

        return new SuccessDataResult<List<CampaignListDTO>>(campaignListDTOs, "Kampanyalar listelendi");
    }

    public async Task<IDataResult<CampaignDTO>> GetByIdAsync(Guid id)
    {
        var campaign = await _campaignRepository.GetByIdAsync(id);
        if (campaign == null)
        {
            return new ErrorDataResult<CampaignDTO>(null, "Kampanya bulunamadı");
        }
        return new SuccessDataResult<CampaignDTO>(campaign.Adapt<CampaignDTO>(), "Kampanya bulundu");
    }

    public async Task<IDataResult<List<ProductDTO>>> GetProductByCampaignIdAsync(Guid campaignId)
    {
        var products = await _productRepository.GetAllAsync(m => m.ProductCampaigns.Any(pm => pm.CampaignId == campaignId && pm.Status != Status.Deleted));
        if (products == null || !products.Any())
        {
            return new SuccessDataResult<List<ProductDTO>>(new List<ProductDTO>(), "Ürün Bulunamadı");
        }

        var productsDTOs = products.Adapt<List<ProductDTO>>();
        return new SuccessDataResult<List<ProductDTO>>(products.Adapt<List<ProductDTO>>(), "Ürün Bulundu");
    }

    public async Task<IDataResult<CampaignDTO>> UpdateAsync(CampaignUpdateDTO campaignUpdateDTO)
    {
        var existingCampaign = await _campaignRepository.GetByIdAsync(campaignUpdateDTO.Id);
        if (existingCampaign == null)
        {
            return new ErrorDataResult<CampaignDTO>("Güncellenecek Kampanya Bulunamadı");
        }

        if (await _campaignRepository.AnyAsync(x => x.Name.ToLower() == campaignUpdateDTO.Name.ToLower() && x.Id != campaignUpdateDTO.Id))
        {
            return new ErrorDataResult<CampaignDTO>("Girilen kampanya sistemde mevcut!");
        }

        DataResult<CampaignDTO> result = new ErrorDataResult<CampaignDTO>();
        var strategy = await _campaignRepository.CreateExecutionStrategy().ConfigureAwait(false);
        await strategy.ExecuteAsync(async () =>
        {
            var transaction = await _campaignRepository.BeginTransactionAsync();
            try
            {
                existingCampaign.Name = campaignUpdateDTO.Name;
                existingCampaign.Description = campaignUpdateDTO.Description;
                existingCampaign.Discount = campaignUpdateDTO.Discount;
                existingCampaign.Image = campaignUpdateDTO.Image;
                existingCampaign.StartDate = campaignUpdateDTO.StartDate;
                existingCampaign.EndDate = campaignUpdateDTO.EndDate;

                var existingProducts = await _productCampaignRepository.GetAllAsync(pm => pm.CampaignId == existingCampaign.Id);
                foreach (var product in existingProducts)
                {
                    await _productCampaignRepository.DeleteAsync(product);
                }

                foreach (var productId in campaignUpdateDTO.ProductIds)
                {
                    var productCampaign = new ProductCampaign
                    {
                        CampaignId = existingCampaign.Id,
                        ProductId = productId
                    };

                    await _productCampaignRepository.AddAsync(productCampaign);
                }

                await _campaignRepository.UpdateAsync(existingCampaign);
                await _campaignRepository.SaveChangesAsync();
                await transaction.CommitAsync();

                result = new SuccessDataResult<CampaignDTO>(existingCampaign.Adapt<CampaignDTO>(), "Kampanya Güncelleme Başarılı");
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<CampaignDTO>("Kampanya güncelleme sırasında bir hata oluştu: " + ex.Message);
                await transaction.RollbackAsync();
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        return result;
    }

    public async Task ActivateCampaign(Guid campaignId)
    {
        var campaign = await _campaignRepository.GetByIdAsync(campaignId);
        if (campaign != null)
        {
            campaign.Status = (Status)CampaignStatus.Active; // Enum dönüşümünü burada düzeltin
            var products = await _productRepository.GetAllAsync(p => p.ProductCampaigns.Any(pc => pc.CampaignId == campaignId)); // Ürünleri ilişkilendirin

            foreach (var product in products)
            {
                product.Price *= (1 - campaign.Discount / 100);
                await _productRepository.UpdateAsync(product);
            }

            await _campaignRepository.UpdateAsync(campaign);
            await _campaignRepository.SaveChangesAsync();
        }
    }

    public async Task DeactivateCampaign(Guid campaignId)
    {
        var campaign = await _campaignRepository.GetByIdAsync(campaignId);
        if (campaign != null)
        {
            campaign.Status = (Status)CampaignStatus.Finished; // Enum dönüşümünü burada düzeltin
            var products = await _productRepository.GetAllAsync(p => p.ProductCampaigns.Any(pc => pc.CampaignId == campaignId)); // Ürünleri ilişkilendirin

            foreach (var product in products)
            {
                product.Price /= (1 - campaign.Discount / 100);
                await _productRepository.UpdateAsync(product);
            }

            await _campaignRepository.UpdateAsync(campaign);
            await _campaignRepository.SaveChangesAsync();
        }
    }

    public async Task UpdateCampaignStatusAndPrices()
    {
        var campaigns = await _campaignRepository.GetAllAsync();
        var currentDate = DateTime.Now;

        foreach (var campaign in campaigns)
        {
            if (currentDate < campaign.StartDate)
            {
                campaign.CampaignStatus = CampaignStatus.Pending;
            }
            else if (currentDate >= campaign.StartDate && currentDate <= campaign.EndDate)
            {
                campaign.CampaignStatus = CampaignStatus.Active;

                // Kampanya aktifse indirimli fiyatları güncelle
                var products = await _productRepository.GetAllAsync(p => p.ProductCampaigns.Any(pc => pc.CampaignId == campaign.Id));
                foreach (var product in products)
                {
                    product.Price *= (1 - campaign.Discount / 100);
                    await _productRepository.UpdateAsync(product);
                }
            }
            else
            {
                campaign.CampaignStatus = CampaignStatus.Finished;

                // Kampanya bitmişse orijinal fiyatları geri yükle
                var products = await _productRepository.GetAllAsync(p => p.ProductCampaigns.Any(pc => pc.CampaignId == campaign.Id));
                foreach (var product in products)
                {
                    product.Price /= (1 - campaign.Discount / 100);
                    await _productRepository.UpdateAsync(product);
                }
            }

            await _campaignRepository.UpdateAsync(campaign);
            await _campaignRepository.SaveChangesAsync();
        }
    }

}