using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.Services.ProductsServices.CampaignServices;
using RestaurantApp.Application.Services.ProductsServices.ProductServices;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductCampaignRepositories;
using RestaurantApp.UI.Areas.Admin.Models.CampaignVMs;

namespace RestaurantApp.UI.Controllers
{
    public class CampaignController : BaseController
    {
        private readonly ICampaignService _campaignService;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        public CampaignController(ICampaignService campaignService, IStringLocalizer<ModelResource> stringLocalizer)
        {
            _campaignService = campaignService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _campaignService.GetAllAsync();
            var campaignVMs = result.Data.Adapt<List<AdminCampaignListVM>>();

            foreach (var campaign in campaignVMs)
            {
                var productsResult = await _campaignService.GetProductByCampaignIdAsync(campaign.Id);
                if (productsResult.IsSuccess)
                {
                    campaign.ProductNames = productsResult.Data.Select(m => m.Name).ToList();
                }
            }

            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["Campaign not found!"], notificationType:UI.Models.NotificationType.error);

                return View(new List<AdminCampaignListVM>());
            }


            return View(campaignVMs);
        }
    }
}
