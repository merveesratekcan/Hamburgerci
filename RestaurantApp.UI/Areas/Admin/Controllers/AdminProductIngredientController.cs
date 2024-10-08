using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductIngredientDTOs;
using RestaurantApp.Application.Services.ProductsServices.IngredientServices;
using RestaurantApp.Application.Services.ProductsServices.ProductIngredientServices;
using RestaurantApp.Application.Services.ProductsServices.ProductServices;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.UI.Areas.Admin.Models.ProductIngredientVMs;
using RestaurantApp.UI.Areas.Admin.Models.ProductVMs;
namespace RestaurantApp.UI.Areas.Admin.Controllers;

public class AdminProductIngredientController : AdminBaseController
{
    private readonly IProductIngredientService _productIngredientService;
    private readonly IIngredientService _ingredientService;
    private readonly IStringLocalizer<ModelResource> _stringLocalizer;
    private readonly IProductService _productService;

    public AdminProductIngredientController(IProductIngredientService productIngredientService, IProductService productService, IStringLocalizer<ModelResource> stringLocalizer, IIngredientService ingredientService)
    {
        _productIngredientService = productIngredientService;
        _stringLocalizer = stringLocalizer;
        _ingredientService = ingredientService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _productIngredientService.GetAllAsync();

        if (!result.IsSuccess)
        {
            NotifyError(_stringLocalizer["ProductIngredient not found!"]);
            return View(new Dictionary<string, List<AdminProductIngredientListVM>>());
        }
        var productIngredientVms = result.Data.Adapt<List<AdminProductIngredientListVM>>();

        foreach (var productIngredient in productIngredientVms)
        {
            var ingredientResult = await _ingredientService.GetByIdAsync(productIngredient.IngredientsId);
            if (ingredientResult.IsSuccess)
            {
                productIngredient.IngredientName = ingredientResult.Data.Name;
            }
            var productResult = await _productService.GetByIdAsync(productIngredient.ProductId);
            if (productResult.IsSuccess)
            {
                productIngredient.ProductName = productResult.Data.Name;
            }
            if (!productIngredient.IsOptional)
            {
                productIngredient.IngredientPrice = 0;
            }
        }
       
        var groupedProductIngredients = productIngredientVms
            .Where(p => p.IsOptional)
            .GroupBy(p => p.ProductName)
            .ToDictionary(g => g.Key, g => g.ToList());

        NotifySuccess(_stringLocalizer["ProductIngredient List successfully!"]);
        return View(groupedProductIngredients);
    }



    public async Task<IActionResult> Update(Guid id)
    {
        var result = await _productIngredientService.GetByIdAsync(id);
        if (result is null)
        {
            NotifyError(_stringLocalizer["ProductIngredient Not Found"]);
            return View();
        }
       
        var productIngredientUpdateVM = result.Data.Adapt<AdminProductIngredientUpdateVM>();
        if (!productIngredientUpdateVM.IsOptional)
        {
            productIngredientUpdateVM.IngredientPrice = 0;
        }
        productIngredientUpdateVM.Products = await GetProducts();
        productIngredientUpdateVM.Ingredients = await GetIngredients();
        return PartialView("/Areas/Admin/Views/AdminProductIngredient/Partials/_UpdatePartial.cshtml", productIngredientUpdateVM);

    }

    [HttpPost]
    public async Task<IActionResult> Update(AdminProductIngredientUpdateVM model)
    {
        if (ModelState.IsValid)
        {
            var productIngredient = model.Adapt<ProductIngredientUpdateDTO>();

            var result = await _productIngredientService.UpdateAsync(productIngredient);
            if (!result.IsSuccess)
            {
                NotifyError(_stringLocalizer["Update failed"]);
                model.Products = await GetProducts();
                model.Ingredients = await GetIngredients();
                return PartialView("/Areas/Admin/Views/AdminProductIngredient/Partials/_UpdatePartial.cshtml", model);
            }
            if (!productIngredient.IsOptional)
            {
                productIngredient.IngredientPrice = 0;
            }
            NotifySuccess(_stringLocalizer["Update Success"]);
            return RedirectToAction("Index");


        }
        model.Products = await GetProducts();
        model.Ingredients = await GetIngredients();
        return RedirectToAction("Index");
    }

    private async Task<SelectList> GetIngredients(Guid? ingredientId = null)
    {
        var ingredient = (await _ingredientService.GetAllAsync()).Data;
        return new SelectList(ingredient.Select(src => new SelectListItem
        {
            Value = src.Id.ToString(),
            Text = src.Name,
            Selected = src.Id == (ingredientId != null ? ingredientId.Value : ingredientId)

        }).OrderBy(src => src.Text), "Value", "Text");
    }

    private async Task<SelectList> GetProducts(Guid? productId = null)
    {
        var product = (await _productService.GetAllAsync()).Data;
       return new SelectList(product
        .OrderBy(src => src.Name)  // Ürün adlarını alfabetik olarak sıralar
        .Select(src => new SelectListItem
        {
            Value = src.Id.ToString(),
            Text = src.Name,
            Selected = src.Id == (productId != null ? productId.Value : productId)
        }), "Value", "Text");
    }

   

}
