using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;
using RestaurantApp.Application.Services.ProductsServices.IngredientServices;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductIngredientRepositories;
using RestaurantApp.UI.Areas.Admin.Models.CategoryVMs;
using RestaurantApp.UI.Areas.Admin.Models.IngredientVMs;

namespace RestaurantApp.UI.Areas.Admin.Controllers;

public class AdminIngredientController : AdminBaseController
{
    private readonly IIngredientService _ingredientService;
    private readonly IProductIngredientRepository _productIngredientRepository;
    private readonly IStringLocalizer<ModelResource> _stringLocalizer;

    public AdminIngredientController(IIngredientService ingredientService, IStringLocalizer<ModelResource> stringLocalizer, IProductIngredientRepository productIngredientRepository)
    {
        _ingredientService = ingredientService;
        _productIngredientRepository = productIngredientRepository;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _ingredientService.GetAllAsync();
        if (!result.IsSuccess)
        {

            NotifyError(_stringLocalizer["Ingredients not found!"]);
            return View(result.Data.Adapt<List<AdminIngredientListVM>>());
        }

        NotifySuccess(_stringLocalizer["Ingredient List success!"]);
        return View(result.Data.Adapt<List<AdminIngredientListVM>>());
    }

    public async Task<IActionResult> Create()
    {
        return PartialView("/Areas/Admin/Views/AdminIngredient/Partials/_CreatePartial.cshtml", new AdminIngredientCreateVM());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AdminIngredientCreateVM model)
    {
        if (ModelState.IsValid)
        {
            var result = await _ingredientService.CreateAsync(model.Adapt<IngredientCreateDTO>());
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);

                return RedirectToAction("Index");
            }
            Notify(_stringLocalizer["Success"], notificationType: UI.Models.NotificationType.success);

            return RedirectToAction("Index");
        }
        Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(Guid id)
    {
        var result = await _ingredientService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {

            NotifyError(_stringLocalizer["Ingredient not found!"]);
            return View();
        }

        return PartialView("/Areas/Admin/Views/AdminIngredient/Partials/_UpdatePartial.cshtml", result.Data.Adapt<AdminIngredientUpdateVM>());
    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminIngredientUpdateVM model)
    {
        if (ModelState.IsValid)
        {
            var result = await _ingredientService.UpdateAsync(model.Adapt<IngredientUpdateDTO>());
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["Update failed!"], notificationType: UI.Models.NotificationType.error);

                return RedirectToAction("Index");
            }
            Notify(_stringLocalizer["Success"], notificationType: UI.Models.NotificationType.success);

            return RedirectToAction("Index");
        }
        Notify(_stringLocalizer["Update failed!"], notificationType: UI.Models.NotificationType.error);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {

        var ingredient = await _ingredientService.GetByIdAsync(id);
        if (ingredient.Data == null)
        {
            Notify(_stringLocalizer["Ingredient not found!"], notificationType: UI.Models.NotificationType.error);
            return RedirectToAction("Index");
        }
        if (await _productIngredientRepository.AnyAsync(pi => pi.IngredientId == id))
        {
            Notify(_stringLocalizer["Ingredient is used in a product, cannot be deleted!"], notificationType: UI.Models.NotificationType.error);
            return RedirectToAction("Index");
        }
        var result = await _ingredientService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            Notify(_stringLocalizer["Delete failed!"], notificationType: UI.Models.NotificationType.error);

        }
        Notify(_stringLocalizer["Success"], notificationType: UI.Models.NotificationType.success);
        return RedirectToAction("Index");

    }
}


