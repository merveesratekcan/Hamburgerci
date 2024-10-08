using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.UsersDTOs.AdminDTOs;
using RestaurantApp.Application.DTOs.UsersDTOs.AppUserDTOs;
using RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;
using RestaurantApp.Application.Services.UserServices.AppUserServices;
using RestaurantApp.UI.Areas.Admin.Models.AdminVMs;
using RestaurantApp.UI.Areas.Admin.Models.AppUserVMs;
using RestaurantApp.UI.Areas.Admin.Models.CategoryVMs;
using RestaurantApp.UI.Models;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{

    public class AdminAppUserController : AdminBaseController
    {
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        private readonly IAppUserService _appUserService;

        public AdminAppUserController(IAppUserService appUserService, IStringLocalizer<ModelResource> stringLocalizer)
        {
            _appUserService = appUserService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _appUserService.GetAllAsync();
            var appUserListVM = result.Data.Adapt<List<AdminAppUserListVM>>();
            if (!result.IsSuccess)
            {

                NotifyError(_stringLocalizer["AppUser not found!"]);
                return View(appUserListVM);
            }
            NotifySuccess(_stringLocalizer["AppUser List successfully!"]);
            return View(appUserListVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminAppUserCreateVM model)
        {
            var appUserCreateDTO = model.Adapt<AppUserCreateDTO>();
            //appUserCreateDTO.Addresses = model.Addresses.Select(address => new UserAddressCreateDTO { Address = address }).ToList();

            var result = await _appUserService.CreateAsync(appUserCreateDTO);
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["Add failed!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["Add failed!"]);
                return View(model);
            }
            Notify(_stringLocalizer["Success"], notificationType: UI.Models.NotificationType.success);
            NotifySuccess(_stringLocalizer["Success"]);
            return RedirectToAction("Index");
        }
    }
}
