using System.Threading.Tasks;
using RestaurantApp.Application.Services.ProductsServices.CampaignServices;
using RestaurantApp.Application.Services.ProductsServices.ProductServices;
using RestaurantApp.Domain.Enums;
using Hangfire;
using RestaurantApp.Application.DTOs.ProductsDTOs.CampaignDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductDTOs;

namespace RestaurantApp.Hangfire.Jobs
{
    public class CampaignJobs
    {
        private readonly ICampaignService _campaignService;
        private readonly IProductService _productService;

        public CampaignJobs(ICampaignService campaignService, IProductService productService)
        {
            _campaignService = campaignService;
            _productService = productService;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task ActivateCampaign(Guid campaignId)
        {
            var campaignResult = await _campaignService.GetByIdAsync(campaignId);
            if (campaignResult.Data == null) return;

            var campaign = campaignResult.Data;
            campaign.Status = CampaignStatus.Active;

            var productCampaigns = await _campaignService.GetProductByCampaignIdAsync(campaignId);
            foreach (var product in productCampaigns.Data)
            {
                product.Price *= (1 - campaign.Discount / 100);
                await _productService.UpdateAsync(new ProductUpdateDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Image
                });
            }

            await _campaignService.UpdateAsync(new CampaignUpdateDTO
            {
                Id = campaign.Id,
                Name = campaign.Name,
                Description = campaign.Description,
                Discount = campaign.Discount,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                Status = campaign.Status
            });
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task DeactivateCampaign(Guid campaignId)
        {
            var campaignResult = await _campaignService.GetByIdAsync(campaignId);
            if (campaignResult.Data == null) return;

            var campaign = campaignResult.Data;
            campaign.Status = CampaignStatus.Finished;

            var productCampaigns = await _campaignService.GetProductByCampaignIdAsync(campaignId);
            foreach (var product in productCampaigns.Data)
            {
                product.Price /= (1 - campaign.Discount / 100);
                await _productService.UpdateAsync(new ProductUpdateDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Image
                });
            }

            await _campaignService.UpdateAsync(new CampaignUpdateDTO
            {
                Id = campaign.Id,
                Name = campaign.Name,
                Description = campaign.Description,
                Discount = campaign.Discount,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                Status = campaign.Status,
            });
        }
    }
}
