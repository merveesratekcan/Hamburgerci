using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.UsersDTOs.AdminDTOs;
using RestaurantApp.Application.Services.UserServices.AdminServices;
using RestaurantApp.Domain.Entities;
using RestaurantApp.UI.Areas.Admin.Models.AdminVMs;
using RestaurantApp.UI.Models;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminController : AdminBaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAdminService _adminService;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;



        public AdminController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IAdminService adminService, IStringLocalizer<ModelResource> stringLocalizer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _adminService = adminService;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<IActionResult> Index()
        {
            var xAdminId = await _userManager.GetUserAsync(User);
            var adminId = await _adminService.GetAdminIdByIdentityId(xAdminId.Id);
            var result = await _adminService.GetByIdAsync(adminId);
            var adminVM = result.Data.Adapt<AdminAdminListVM>();
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["Admin not found!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["Admin not found!"]);
                return View(adminVM);
            }
            return View(adminVM);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminAdminCreateVM model)
        {
            var result = await _adminService.CreateAsync(model.Adapt<AdminCreateDTO>());
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
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _adminService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["No administrator found to update!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["No administrator found to update!"]);
                return RedirectToAction("Index");
            }

            var adminUpdateVM = result.Data.Adapt<AdminAdminUpdateVM>();
            return View(adminUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminAdminUpdateVM model)
        {
            var result = await _adminService.UpdateAsync(model.Adapt<AdminUpdateDTO>());
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["No administrator found to update!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["No administrator found to update!"]);
                return View(model);
            }
            Notify(_stringLocalizer["Success"], notificationType: NotificationType.success);
            NotifySuccess(_stringLocalizer["Success"]);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _adminService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["No administrator found to delete!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["No administrator found to delete!"]);
                return RedirectToAction("Index");
            }
            Notify(_stringLocalizer["Success"], notificationType: NotificationType.success);
            NotifySuccess(_stringLocalizer["Success"]);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangePassword(Guid id)
        {
            var result = await _adminService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["No administrator found to update!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["No administrator found to update!"]);
                return RedirectToAction("Index");
            }

            var adminAdminChangePasswordVM = result.Data.Adapt<AdminAdminChangePasswordVM>();
            return View(adminAdminChangePasswordVM);

        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(AdminAdminChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                Notify(_stringLocalizer["Please check the entries and try again!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["Please check the entries and try again!"]);
                return View(model);
            }
            var result = await _adminService.ChangeAdminPasswordAsync(model.Adapt<AdminChangePasswordDTO>());
            if (!result.Succeeded)
            {
                Notify(_stringLocalizer["Password cannot changed!"], notificationType: NotificationType.error);
                NotifyError(_stringLocalizer["Password cannot changed!"]);
                return RedirectToAction("Index");
            }
            Notify(_stringLocalizer["Password changed successfully!"], notificationType: NotificationType.success);
            NotifySuccess(_stringLocalizer["Password changed successfully!"]);
            return RedirectToAction("Index");

        }
        public IActionResult UpdatePasswordConfirmation()
        {
            return View();
        }
    }
}
