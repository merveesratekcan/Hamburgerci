using Mapster;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductDTOs;
using RestaurantApp.Domain.Contracts.ProductContracts.IngredientRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductIngredientRepositories;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Enums;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;


namespace RestaurantApp.Application.Services.ProductsServices.ProductServices;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IProductIngredientRepository _productIngredientRepository;

    public ProductService(IProductRepository productRepository, IIngredientRepository ingredientRepository, IProductIngredientRepository productIngredientRepository)
    {
        _productRepository = productRepository;
        _ingredientRepository = ingredientRepository;
        _productIngredientRepository = productIngredientRepository;
    }

    public async Task<IDataResult<ProductDTO>> AddAsync(ProductCreateDTO productCreateDTO)
    {
        if (await _productRepository.AnyAsync(x => x.Name.ToLower() == productCreateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<ProductDTO>("Mevcut ürün sistemde kayıtlı!");
        }

        var newProduct = productCreateDTO.Adapt<Product>();
        newProduct.ProductIngredients = productCreateDTO.IngredientsId.Select(ingredientId => new ProductIngredient
        {
            ProductId = newProduct.Id,
            IngredientId = ingredientId.IngredientsId,
            IsOptional = ingredientId.IsOptional
        }).ToList();

        DataResult<ProductDTO> result = new ErrorDataResult<ProductDTO>("Ürün Ekleme Başarısız!");

        var strategy = await _productRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _productRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                await _productRepository.AddAsync(newProduct);
                await _productRepository.SaveChangesAsync();
                result = new SuccessDataResult<ProductDTO>(newProduct.Adapt<ProductDTO>(), "Ürün Ekleme Başarılı!");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<ProductDTO>("Ürün Ekleme Başarısız: " + ex.Message);
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
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return new ErrorResult("Ürün bulunamadı");
        }
        try
        {
            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();
            return new SuccessResult("Ürün silme işlemi başarılı");
        }
        catch (Exception)
        {
            return new ErrorResult("Ürün silme işlemi sırasında bir hata oluştu");
        }
    }

    public async Task<IDataResult<List<ProductListDTO>>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        if (products.Count() <= 0)
        {
            return new ErrorDataResult<List<ProductListDTO>>(products.Adapt<List<ProductListDTO>>(), "Listelenecek ürün bulunamadı!");
        }
        return new SuccessDataResult<List<ProductListDTO>>(products.Adapt<List<ProductListDTO>>(), "Ürünler listelendi");
    }

    public async Task<IDataResult<List<ProductListDTO>>> GetAllByProductIdAsync(Guid categoryId)
    {
        var products = await _productRepository.GetAllAsync(x => x.CategoryId == categoryId);
        if (products.Count() <= 0)
        {
            return new ErrorDataResult<List<ProductListDTO>>(products.Adapt<List<ProductListDTO>>(), "Listelenecek Ürün Bulunamadı");
        }
        return new SuccessDataResult<List<ProductListDTO>>(products.Adapt<List<ProductListDTO>>(), "Ürün Listeleme Başarılı");
    }

    public async Task<IDataResult<ProductDTO>> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return new ErrorDataResult<ProductDTO>(product.Adapt<ProductDTO>(), "Ürün bulunamadı");
        }
        return new SuccessDataResult<ProductDTO>(product.Adapt<ProductDTO>(), "Ürün bulundu");
    }


    public async Task<IDataResult<List<IngredientDTO>>> GetIngredientsByProductIdAsync(Guid productId)
    {
        var ingredients = await _ingredientRepository.GetAllAsync(m => m.ProductIngredients.Any(pm => pm.ProductId == productId && pm.Status != Status.Deleted));
        if (ingredients == null || !ingredients.Any())
        {
            return new SuccessDataResult<List<IngredientDTO>>(new List<IngredientDTO>(), "Malzeme Bulunamadı");
        }
        var ingredientDTOs = ingredients.Select(ingredient => new IngredientDTO
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            IsOptional = ingredient.ProductIngredients.FirstOrDefault(pm => pm.ProductId == productId).IsOptional,
            OptionalPrice = ingredient.ProductIngredients.FirstOrDefault(pm => pm.ProductId == productId).IngredientPrice
            
        }).ToList();
        return new SuccessDataResult<List<IngredientDTO>>(ingredientDTOs, "Malzeme Bulundu");
    }

    public async Task<IDataResult<ProductDTO>> UpdateAsync(ProductUpdateDTO productUpdateDTO)
    {
        DataResult<ProductDTO> result = new ErrorDataResult<ProductDTO>();
        var strategy = await _productRepository.CreateExecutionStrategy().ConfigureAwait(false);
        await strategy.ExecuteAsync(async () =>
        {
            var transaction = await _productRepository.BeginTransactionAsync();
            try
            {
                var updatingProduct = await _productRepository.GetByIdAsync(productUpdateDTO.Id);
                if (updatingProduct == null)
                {
                    result = new ErrorDataResult<ProductDTO>("Güncellenecek Ürün Bulunamadı");
                    await transaction.RollbackAsync();
                    return;
                }

                updatingProduct.Name = productUpdateDTO.Name;
                updatingProduct.Price = productUpdateDTO.Price;
                updatingProduct.Description = productUpdateDTO.Description;
                updatingProduct.CategoryId = productUpdateDTO.CategoryId;
                updatingProduct.Image = productUpdateDTO.Image;

                var existingIngredients = await _productIngredientRepository.GetAllAsync(pm => pm.ProductId == updatingProduct.Id);
                var newIngredients = productUpdateDTO.IngredientsId;

                var ingredientsToRemove = existingIngredients
                    .Where(ei => !newIngredients.Any(ni => ni.IngredientsId == ei.IngredientId))
                    .ToList();

                var ingredientsToAdd = newIngredients
                    .Where(ni => !existingIngredients.Any(ei => ei.IngredientId == ni.IngredientsId))
                    .Select(ni => new ProductIngredient
                    {
                        ProductId = updatingProduct.Id,
                        IngredientId = ni.IngredientsId,
                        IsOptional = ni.IsOptional
                    })
                    .ToList();

                foreach (var existingIngredient in existingIngredients)
                {
                    var newIngredient = newIngredients.FirstOrDefault(ni => ni.IngredientsId == existingIngredient.IngredientId);
                    if (newIngredient != null)
                    {
                        existingIngredient.IsOptional = newIngredient.IsOptional;
                    }
                }

                foreach (var ingredientToRemove in ingredientsToRemove)
                {
                    await _productIngredientRepository.DeleteAsync(ingredientToRemove);
                }

                foreach (var ingredientToAdd in ingredientsToAdd)
                {
                    await _productIngredientRepository.AddAsync(ingredientToAdd);
                }

                await _productRepository.UpdateAsync(updatingProduct);

                var affectedRows = await _productRepository.SaveChangesAsync();
                if (affectedRows == 0)
                {
                    result = new ErrorDataResult<ProductDTO>("Güncelleme sırasında beklenmedik bir sorun oluştu.");
                    await transaction.RollbackAsync();
                    return;
                }

                await transaction.CommitAsync();

                result = new SuccessDataResult<ProductDTO>(updatingProduct.Adapt<ProductDTO>(), "Ürün Güncelleme Başarılı");
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<ProductDTO>("Ürün güncelleme sırasında bir hata oluştu: " + ex.Message);
                await transaction.RollbackAsync();
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        return result;
    }
   

}





