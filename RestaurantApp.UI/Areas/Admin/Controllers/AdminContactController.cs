using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.ProductsDTOs.CategoryDTOs;
using RestaurantApp.Application.DTOs.WebDTOs.ContactDTOs;
using RestaurantApp.Application.Services.WebServices.ContactServices;
using RestaurantApp.UI.Areas.Admin.Models.CategoryVMs;
using RestaurantApp.UI.Areas.Admin.Models.ContactVMs;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminContactController : AdminBaseController
    {
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        private readonly IContactService _contactService;

        public AdminContactController(IStringLocalizer<ModelResource> stringLocalizer, IContactService contactService)
        {
            _stringLocalizer = stringLocalizer;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _contactService.GetAllAsync();
            if (!result.IsSuccess)
            {
                NotifyError(_stringLocalizer["Contact not found!"]);
                return View(result.Data.Adapt<List<AdminContactListVM>>());
            }
            NotifySuccess(_stringLocalizer["Contact List successfully!"]);
            return View(result.Data.Adapt<List<AdminContactListVM>>());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminContactCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.AddAsync(model.Adapt<ContactCreateDTO>());
                if (!result.IsSuccess)
                {
                    NotifyError(_stringLocalizer["Add failed!"]);
                    return View(model);
                }
                NotifySuccess(_stringLocalizer["Success"]);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _contactService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                NotifyError(_stringLocalizer["Contact not found!"]);
                return RedirectToAction("Index");
            }
            return View(result.Data.Adapt<AdminContactUpdateVM>());
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminContactUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactService.UpdateAsync(model.Adapt<ContactUpdateDTO>());
                if (!result.IsSuccess)
                {
                    NotifyError(_stringLocalizer["Update failed!"]);
                    return View(model);
                }
                NotifySuccess(_stringLocalizer["Success"]);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _contactService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                NotifyError(_stringLocalizer["Delete failed!"]);
            }
            NotifySuccess(_stringLocalizer["Success"]);
            return RedirectToAction("Index");
        }
    }
}
