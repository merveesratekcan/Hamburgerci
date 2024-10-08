using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.ProductsDTOs.CampaignDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductDTOs;
using RestaurantApp.Application.Services.ProductsServices.CampaignServices;
using RestaurantApp.Application.Services.ProductsServices.ProductServices;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductCampaignRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.UI.Areas.Admin.Models.CampaignVMs;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminCampaignController : AdminBaseController
    {
        private readonly ICampaignService _campaignService;
        private readonly IProductService _productService;
        private readonly IProductCampaignRepository _productCampaignRepository;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        public AdminCampaignController(ICampaignService campaignService, IProductService productService, IProductCampaignRepository productCampaignRepository, IStringLocalizer<ModelResource> stringLocalizer)
        {
            _campaignService = campaignService;
            _productService = productService;
            _productCampaignRepository = productCampaignRepository;
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
                //Notify(_stringLocalizer["Campaign not found!"], notificationType:UI.Models.NotificationType.error);
                NotifyError(_stringLocalizer["Campaign not found!"]);
                return View(new List<AdminCampaignListVM>());
            }

            NotifySuccess(result.Messages);
            return View(campaignVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var campaignVM = new AdminCampaignCreateVM
            {
                Products = await GetProducts()
            };
            return PartialView("/Areas/Admin/Views/AdminCampaign/Partials/_CreatePartial.cshtml", campaignVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCampaignCreateVM campaignCreateVM)
        {

            if (!ModelState.IsValid)
            {
                campaignCreateVM.Products = await GetProducts();
                //Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);
                NotifyError(_stringLocalizer["Add failed!"]);
                return RedirectToAction("Index");
            }

            var campaignCreateDTO = campaignCreateVM.Adapt<CampaignCreateDTO>();
            if (campaignCreateVM.NewImage != null && campaignCreateVM.NewImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await campaignCreateVM.NewImage.CopyToAsync(memoryStream);
                    campaignCreateDTO.Image = memoryStream.ToArray();
                }
            }

            if (campaignCreateVM.EndDate <= campaignCreateVM.StartDate)
            {
                campaignCreateVM.Products = await GetProducts();
                Notify("The campaign start date must be before the end date!", notificationType: UI.Models.NotificationType.error);
                //NotifyError(_stringLocalizer["The campaign start date must be before the end date!"]);
                return RedirectToAction("Index"); ;
            }
            if (campaignCreateVM.StartDate < DateTime.UtcNow)
            {
                campaignCreateVM.Products = await GetProducts();
                Notify("The campaign cannot be applied retroactively!", notificationType: UI.Models.NotificationType.error);
                //NotifyError(_stringLocalizer["The campaign cannot be applied retroactively!"]);
                return RedirectToAction("Index");
            }

            campaignCreateDTO.ProductIds = campaignCreateVM.SelectedProducts;

            var result = await _campaignService.AddAsync(campaignCreateDTO);
            if (!result.IsSuccess)
            {
                campaignCreateVM.Products = await GetProducts();
                //Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);
                NotifyError(_stringLocalizer["Add failed!"]);
                return RedirectToAction("Index");
            }
            //Notify(_stringLocalizer["Campaign Created Succesfully!"], notificationType: UI.Models.NotificationType.success);
            NotifySuccess(_stringLocalizer["Campaign Created Succesfully!"]);
            return RedirectToAction("Index");


        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _campaignService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                //Notify(_stringLocalizer["Campaign not found!"], notificationType: UI.Models.NotificationType.error);
                NotifyError(_stringLocalizer["Campaign not found!"]);
                return RedirectToAction("Index");
            }

            var campaign = result.Data;
            var campaignUpdateVM = campaign.Adapt<AdminCampaignUpdateVM>();
            campaignUpdateVM.ExistingImage = campaign.Image != null ? Convert.ToBase64String(campaign.Image) : string.Empty;

            var productsResult = await _productService.GetAllAsync();
            var products = productsResult.Data;

            var productCampaigns = await _productCampaignRepository.GetAllAsync(pm => pm.CampaignId == campaign.Id);
            var selectedProductIds = productCampaigns.Select(pm => pm.ProductId).ToList();

            campaignUpdateVM.Products = products.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name,
                Selected = selectedProductIds.Contains(m.Id)
            }).ToList();

            campaignUpdateVM.SelectedProducts = selectedProductIds;

            return PartialView("/Areas/Admin/Views/AdminCampaign/Partials/_UpdatePartial.cshtml", campaignUpdateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AdminCampaignUpdateVM model)
        {
            if (model.EndDate <= model.StartDate)
            {
                model.Products = await GetProducts();
                Notify("The campaign start date must be before the end date!", notificationType: UI.Models.NotificationType.error);
                //NotifyError(_stringLocalizer["The campaign start date must be before the end date!"]);
                return RedirectToAction("Index", model);
            }
            if (model.StartDate < DateTime.UtcNow)
            {
                model.Products = await GetProducts();
                Notify("The campaign cannot be applied retroactively!", notificationType: UI.Models.NotificationType.error);
                //NotifyError(_stringLocalizer["The campaign cannot be applied retroactively!"]);
                return RedirectToAction("Index", model);
            }

            if (ModelState.IsValid)
            {
                var campaignUpdateDTO = model.Adapt<CampaignUpdateDTO>();
                var campaignExisting = await _campaignService.GetByIdAsync(campaignUpdateDTO.Id);

                if (model.SelectedProducts == null || !model.SelectedProducts.Any())
                {
                    //Notify("No products selected.", notificationType: UI.Models.NotificationType.error);
                    NotifyError(_stringLocalizer["No products selected."]);

                    model.Products = await GetProducts();
                    return PartialView("/Areas/Admin/Views/AdminCampaign/Partials/_UpdatePartial.cshtml", model);
                }

                if (model.NewImage != null && model.NewImage.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.NewImage.CopyToAsync(memoryStream);
                        campaignUpdateDTO.Image = memoryStream.ToArray();
                    }
                }
                else
                {
                    campaignUpdateDTO.Image = campaignExisting.Data.Image;
                }

                campaignUpdateDTO.StartDate = model.StartDate;
                campaignUpdateDTO.EndDate = model.EndDate;
                campaignUpdateDTO.ProductIds = model.SelectedProducts;

                var result = await _campaignService.UpdateAsync(campaignUpdateDTO);

                if (!result.IsSuccess)
                {
                    //Notify(_stringLocalizer["Update failed!"], notificationType: UI.Models.NotificationType.error);
                    NotifyError(_stringLocalizer["Update failed!"]);
                    model.Products = await GetProducts();
                    return PartialView("/Areas/Admin/Views/AdminCampaign/Partials/_UpdatePartial.cshtml", model);
                }



                var existingProductCampaigns = await _productCampaignRepository.GetAllAsync(pm => pm.CampaignId == campaignUpdateDTO.Id);
                var existingProductIds = existingProductCampaigns.Select(pm => pm.ProductId).ToList();

                foreach (var productCampaign in existingProductCampaigns)
                {
                    if (!model.SelectedProducts.Contains(productCampaign.ProductId))
                    {
                        await _productCampaignRepository.DeleteAsync(productCampaign);
                    }
                }

                foreach (var productId in model.SelectedProducts)
                {
                    if (!existingProductIds.Contains(productId))
                    {
                        await _productCampaignRepository.AddAsync(new ProductCampaign
                        {
                            CampaignId = campaignUpdateDTO.Id,
                            ProductId = productId
                        });
                    }
                }

                //Notify(_stringLocalizer["Success"], notificationType: UI.Models.NotificationType.success);
                NotifySuccess(_stringLocalizer["Success"]);
                return RedirectToAction("Index");
            }

            model.Products = await GetProducts();
            return PartialView("/Areas/Admin/Views/AdminCampaign/Partials/_UpdatePartial.cshtml", model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _campaignService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                //Notify(_stringLocalizer["Delete failed!"], notificationType: UI.Models.NotificationType.error);
                NotifyError(_stringLocalizer["Delete failed!"]);
                return RedirectToAction("Index");
            }
            //Notify(_stringLocalizer["Success"], notificationType: UI.Models.NotificationType.success);
            NotifySuccess(_stringLocalizer["Success"]);
            return RedirectToAction("Index");
        }

        public async Task<List<SelectListItem>> GetProducts(List<Guid>? selectedProductIds = null)
        {
            var productsResult = await _productService.GetAllAsync();
            var products = productsResult.Data ?? new List<ProductListDTO>();
            return products.Select(src => new SelectListItem
            {
                Value = src.Id.ToString(),
                Text = src.Name,
                Selected = selectedProductIds != null && selectedProductIds.Contains(src.Id)
            }).OrderBy(x => x.Text).ToList();
        }
    }
}