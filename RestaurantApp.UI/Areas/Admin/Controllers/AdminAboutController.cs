using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using RestaurantApp.Application.DTOs.WebDTOs.AboutDTOs;
using RestaurantApp.Application.DTOs.WebDTOs.FeatureDTOs;
using RestaurantApp.Application.Services.OrderServices.AboutServices;
using RestaurantApp.UI.Areas.Admin.Models.AboutVMs;
using RestaurantApp.UI.Areas.Admin.Models.FeatureVMs;
using RestaurantApp.UI.Areas.Admin.Models.ProductVMs;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminAboutController : AdminBaseController
    {
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        private readonly IAboutService _aboutService;

        public AdminAboutController(IStringLocalizer<ModelResource> stringLocalizer, IAboutService aboutService)
        {
            _stringLocalizer = stringLocalizer;
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _aboutService.GetAllAsync();
            if (!result.IsSuccess)
            {
                NotifyError(_stringLocalizer["About not found!"]);
                return View(result.Data.Adapt<List<AdminAboutListVM>>());
            }
            NotifySuccess(_stringLocalizer["About List successfully!"]);
            return View(result.Data.Adapt<List<AdminAboutListVM>>());

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminAboutCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _aboutService.AddAsync(model.Adapt<AboutCreateDTO>());
                if (!result.IsSuccess)
                {
                    Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);
                    return RedirectToAction("Index");
                }
                var aboutCreateDTO = model.Adapt<AboutCreateDTO>();
               
                Notify(_stringLocalizer["About Created Succesfully!"], notificationType: UI.Models.NotificationType.success);
                return RedirectToAction("Index");
            }
            Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _aboutService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                NotifyError(_stringLocalizer["About not found!"]);
                return RedirectToAction("Index");
            }            
            var AboutUpdateVM = result.Data.Adapt<AdminAboutUpdateVM>();
           
            NotifySuccess(_stringLocalizer["About found successfully!"]);
            return View(AboutUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminAboutUpdateVM model)
        {
            if (ModelState.IsValid)
            {
               
                var aboutDTO = model.Adapt<AboutUpdateDTO>();               
                
                var result = await _aboutService.UpdateAsync(aboutDTO);
                if (!result.IsSuccess)
                {
                    Notify(_stringLocalizer["Update failed!"], notificationType: UI.Models.NotificationType.error);
                    return RedirectToAction("Index");
                }
                Notify(_stringLocalizer["About Updated Succesfully!"], notificationType: UI.Models.NotificationType.success);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");





        }
        
    }
}
