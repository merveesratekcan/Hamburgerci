using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;
using RestaurantApp.Application.Services.UserServices.UserAddressServices;
using RestaurantApp.Domain.Contracts.UserContracts.AppUserRepositories;
using RestaurantApp.UI.Areas.Admin.Models.UserAddressVMs;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminUserAddressController : AdminBaseController
    {
        private readonly IUserAddressService _userAddressService;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        public AdminUserAddressController(IUserAddressService userAddressService, IAppUserRepository appUserRepository, IStringLocalizer<ModelResource> stringLocalizer)
        {
            _userAddressService = userAddressService;
            _appUserRepository = appUserRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userAddressService.GetAllAsync();
            if (!result.IsSuccess)
            {

                NotifyError(_stringLocalizer["UserAddress not found!"]);
                return View(result.Data.Adapt<List<AdminUserAddressListVM>>());
            }

            NotifySuccess(_stringLocalizer["UserAddress List successfully!"]);
            return View(result.Data.Adapt<List<AdminUserAddressListVM>>());
        }
        public IActionResult Create()
        {
            return PartialView("/Areas/Admin/Views/AdminUserAddress/Partials/_CreatePartial.cshtml", new AdminUserAddressCreateVM());
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminUserAddressCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAddressService.AddAsync(model.Adapt<UserAddressCreateDTO>());
                if (!result.IsSuccess)
                {
                    Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);

                    return RedirectToAction("Index");
                }
                Notify(_stringLocalizer["UserAddress Created Succesfully!"], notificationType: UI.Models.NotificationType.success);

                return RedirectToAction("Index");
            }
            Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _userAddressService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                //Notify(_stringLocalizer["UserAddress not found!"], notificationType: UI.Models.NotificationType.error);
                NotifyError(_stringLocalizer["UserAddress not found!"]);
                return View();
            }
            return PartialView("/Areas/Admin/Views/AdminUserAddress/Partials/_UpdatePartial.cshtml", result.Data.Adapt<AdminUserAddressUpdateVM>());
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminUserAddressUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userAddressService.UpdateAsync(model.Adapt<UserAddressUpdateDTO>());
                if (!result.IsSuccess)
                {

                    NotifyError(_stringLocalizer["Update failed!"]);
                    return RedirectToAction("Index");
                }
                Notify(_stringLocalizer["Update Success!"], notificationType: UI.Models.NotificationType.success);
                return RedirectToAction("Index");
            }
            Notify(_stringLocalizer["Update failed!"], notificationType: UI.Models.NotificationType.error);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {

            var userAddress = await _userAddressService.GetByIdAsync(id);
            if (userAddress.Data == null)
            {
                Notify(_stringLocalizer["UserAddress not found!"], notificationType: UI.Models.NotificationType.error);
                return RedirectToAction("Index");
            }

            // Kategori kullanılıyorsa silinemez kontrolü
            //if (await _appUserRepository.AnyAsync(p=>p.Addresses))
            //{
            //    Notify(_stringLocalizer["UserAddress is used in a appUser, you can't delete!"], notificationType: UI.Models.NotificationType.error);
            //    return RedirectToAction("Index");
            //}
            var result = await _userAddressService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["Delete failed!"], notificationType: UI.Models.NotificationType.error);
            }
            Notify(_stringLocalizer["Success"], notificationType: UI.Models.NotificationType.success);

            return RedirectToAction("Index");
        }
    }
}
