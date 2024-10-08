using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.WebDTOs.FeatureDTOs;
using RestaurantApp.Application.Services.WebServices.FeatureServices;
using RestaurantApp.UI.Areas.Admin.Models.FeatureVMs;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminFeatureController : Controller
    {
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        private readonly IFeatureService _featureService;

        public AdminFeatureController(IStringLocalizer<ModelResource> stringLocalizer, IFeatureService featureService)
        {
            _stringLocalizer = stringLocalizer;
            _featureService = featureService;
        }

        public async Task<IActionResult> Index()
        {
           var result = await _featureService.GetAllAsync();
            if (!result.IsSuccess)
              {
                //NotifyError(_stringLocalizer["Feature not found!"]);
                return View(result.Data.Adapt<List<AdminFeatureListVM>>());
              }
            //NotifySuccess(_stringLocalizer["Feature List successfully!"]);
            return View(result.Data.Adapt<List<AdminFeatureListVM>>());

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminFeatureCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _featureService.AddAsync(model.Adapt<FeatureCreateDTO>());
                if (!result.IsSuccess)
                {
                    //Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);
                    return RedirectToAction("Index");
                }
                //Notify(_stringLocalizer["Feature Created Succesfully!"], notificationType: UI.Models.NotificationType.success);
                return RedirectToAction("Index");
            }
            //Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id)
        {

           var result = await _featureService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                //NotifyError(_stringLocalizer["Feature not found!"]);
                return RedirectToAction("Index");
            }
            return View(result.Data.Adapt<AdminFeatureUpdateVM>());
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminFeatureUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _featureService.UpdateAsync(model.Adapt<FeatureUpdateDTO>());
                if (!result.IsSuccess)
                {
                    //Notify(_stringLocalizer["Update failed!"], notificationType: UI.Models.NotificationType.error);
                    return RedirectToAction("Index");
                }
                //Notify(_stringLocalizer["Feature Updated Succesfully!"], notificationType: UI.Models.NotificationType.success);
                return RedirectToAction("Index");
            }
            //Notify(_stringLocalizer["Update failed!"], notificationType: UI.Models.NotificationType.error);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _featureService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                //NotifyError(_stringLocalizer["Delete failed!"]);
                return RedirectToAction("Index");
            }
            //Notify(_stringLocalizer["Feature Deleted Succesfully!"], notificationType: UI.Models.NotificationType.success);
            return RedirectToAction("Index");
        }

    }
}
