using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.ProductsDTOs.CategoryDTOs;
using RestaurantApp.Application.Services.ProductsServices.CategoryServices;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductRepositories;
using RestaurantApp.UI.Areas.Admin.Models.CategoryVMs;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminCategoryController : AdminBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;
        public AdminCategoryController(ICategoryService categoryService, IProductRepository productRepository, IStringLocalizer<ModelResource> stringLocalizer)
        {
            _categoryService = categoryService;
            _productRepository = productRepository;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();
            if (!result.IsSuccess)
            {

                NotifyError(_stringLocalizer["Category not found!"]);
                return View(result.Data.Adapt<List<AdminCategoryListVM>>());
            }

            NotifySuccess(_stringLocalizer["Category List successfully!"]);
            return View(result.Data.Adapt<List<AdminCategoryListVM>>());
        }
        public IActionResult Create()
        {
            return PartialView("/Areas/Admin/Views/AdminCategory/Partials/_CreatePartial.cshtml", new AdminCategoryCreateVM());
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminCategoryCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.AddAsync(model.Adapt<CategoryCreateDTO>());
                if (!result.IsSuccess)
                {
                    Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);

                    return RedirectToAction("Index");
                }
                Notify(_stringLocalizer["Category Created Succesfully!"], notificationType: UI.Models.NotificationType.success);

                return RedirectToAction("Index");
            }
            Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                //Notify(_stringLocalizer["Category not found!"], notificationType: UI.Models.NotificationType.error);
                NotifyError(_stringLocalizer["Category not found!"]);
                return View();
            }
            return PartialView("/Areas/Admin/Views/AdminCategory/Partials/_UpdatePartial.cshtml", result.Data.Adapt<AdminCategoryUpdateVM>());
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminCategoryUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateAsync(model.Adapt<CategoryUpdateDTO>());
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

            var category = await _categoryService.GetByIdAsync(id);
            if (category.Data == null)
            {
                Notify(_stringLocalizer["Category not found!"], notificationType: UI.Models.NotificationType.error);
                return RedirectToAction("Index");
            }

            // Kategori kullanılıyorsa silinemez kontrolü
            if (await _productRepository.AnyAsync(p => p.CategoryId == id))
            {
                Notify(_stringLocalizer["Category is used in a product, you can't delete!"], notificationType: UI.Models.NotificationType.error);
                return RedirectToAction("Index");
            }
            var result = await _categoryService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["Delete failed!"], notificationType: UI.Models.NotificationType.error);
            }
            Notify(_stringLocalizer["Success"], notificationType: UI.Models.NotificationType.success);

            return RedirectToAction("Index");
        }
    }
}
