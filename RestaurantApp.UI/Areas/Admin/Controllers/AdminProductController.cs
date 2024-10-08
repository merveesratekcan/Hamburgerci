
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using RestaurantApp.Application.DTOs.ProductMaterialDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductDTOs;
using RestaurantApp.Application.Services.ProductsServices.CategoryServices;
using RestaurantApp.Application.Services.ProductsServices.IngredientServices;
using RestaurantApp.Application.Services.ProductsServices.ProductServices;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductIngredientRepositories;
using RestaurantApp.UI.Areas.Admin.Models.IngredientVMs;
using RestaurantApp.UI.Areas.Admin.Models.ProductVMs;

namespace RestaurantApp.UI.Areas.Admin.Controllers;

public class AdminProductController : AdminBaseController
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IIngredientService _ingredientService;
    private readonly IProductIngredientRepository _productIngredientRepository;
    private readonly IStringLocalizer<ModelResource> _stringLocalizer;

    public AdminProductController(IProductService productService, ICategoryService categoryService, IIngredientService ingredientService, IProductIngredientRepository productIngredientRepository, IStringLocalizer<ModelResource> stringLocalizer)
    {
        _productService = productService;
        _categoryService = categoryService;
        _ingredientService = ingredientService;
        _productIngredientRepository = productIngredientRepository;
        _stringLocalizer = stringLocalizer;
    }

    public async Task<IActionResult> Index()
    {

        var result = await _productService.GetAllAsync();
        if (!result.IsSuccess)
        {
            NotifyError(_stringLocalizer["Product not found!"]);
            return View(new List<ProductListVM>());
        }
        var productVms = result.Data.Adapt<List<ProductListVM>>();
        foreach (var product in productVms)
        {
            var categoryResult = await _categoryService.GetByIdAsync(product.CategoryId);
            if (categoryResult.IsSuccess)
            {
                product.CategoryName = categoryResult.Data.Name;
            }
            var ingredientsResult = await _productService.GetIngredientsByProductIdAsync(product.Id);
            if (ingredientsResult.IsSuccess)
            {
                product.Ingredients = ingredientsResult.Data.Select(m => new AdminIngredientSelectionVM
                {
                    IngredientId = m.Id,
                    IngredientName = m.Name,
                    IsOptional = m.IsOptional,
                }).ToList();
            }
        }
        NotifySuccess(_stringLocalizer["Product List successfully!"]);
        return View(productVms);


    }
    public async Task<IActionResult> Create()
    {
        var productVm = new ProductCreateVM
        {
            Categories = await GetCategory(),
            Ingredients = await GetIngredient()
        };
        return PartialView("/Areas/Admin/Views/AdminProduct/Partials/_CreatePartial.cshtml", productVm);
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVM productCreateVM)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage).ToList();
            foreach (var error in errors)
            {
                Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);
            }
            productCreateVM.Categories = await GetCategory();
            productCreateVM.Ingredients = await GetIngredient();
            return PartialView("/Areas/Admin/Views/AdminProduct/Partials/_CreatePartial.cshtml", productCreateVM);

        }
        var productCreateDTO = productCreateVM.Adapt<ProductCreateDTO>();
        if (productCreateVM.NewImage != null && productCreateVM.NewImage.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await productCreateVM.NewImage.CopyToAsync(memoryStream);
                productCreateDTO.Image = memoryStream.ToArray();
            }
        }
        productCreateDTO.IngredientsId = productCreateVM.Ingredients.Where(m => m.Selected).Select(m => new ProductIngredientDTO
        {
            IngredientsId = m.IngredientId,
            IsOptional = m.IsOptional
        }).ToList();
        var result = await _productService.AddAsync(productCreateDTO);
        if (!result.IsSuccess)
        {
            Notify(_stringLocalizer["Add failed!"], notificationType: UI.Models.NotificationType.error);
            productCreateVM.Categories = await GetCategory();
            productCreateVM.Ingredients = await GetIngredient();
            return PartialView("/Areas/Admin/Views/AdminProduct/Partials/_CreatePartial.cshtml", productCreateVM);
        }
        Notify(_stringLocalizer["Created Succesfully!"], notificationType: UI.Models.NotificationType.success);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Update(Guid id)
    {

        var result = await _productService.GetByIdAsync(id);
        if (!result.IsSuccess)
        {
            NotifyError(_stringLocalizer["Product not found!"]);

            return RedirectToAction("Index");
        }
        var product = result.Data;
        var productUpdateVM = product.Adapt<ProductUpdateVM>();
        productUpdateVM.ExistingImage = product.Image != null ? Convert.ToBase64String(product.Image) : string.Empty;
        productUpdateVM.Categories = await GetCategory(product.CategoryId);

        var ingredientsResult = await _ingredientService.GetAllAsync();
        var ingredients = ingredientsResult.Data;

        var productIngredients = await _productIngredientRepository.GetAllAsync(pm => pm.ProductId == product.Id);
        var selectedIngredients = productIngredients.Select(m => m.IngredientId).ToList();

        productUpdateVM.Ingredients = ingredients.Select(m => new AdminIngredientSelectionVM
        {
            IngredientId = m.Id,
            IngredientName = m.Name,
            Selected = selectedIngredients.Contains(m.Id),
            IsOptional = productIngredients.FirstOrDefault(pm => pm.IngredientId == m.Id)?.IsOptional ?? false
        }).OrderBy(x => x.IngredientName).ToList();

        return PartialView("/Areas/Admin/Views/AdminProduct/Partials/_UpdatePartial.cshtml", productUpdateVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductUpdateVM productUpdateVM)
    {

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage).ToList();
            foreach (var error in errors)
            {
                NotifyError(error);
            }
            productUpdateVM.Categories = await GetCategory(productUpdateVM.CategoryId);
            productUpdateVM.Ingredients = await GetIngredient(productUpdateVM.Ingredients.Select(x => x.IngredientId).ToList());
            return PartialView("/Areas/Admin/Views/AdminProduct/Partials/_UpdatePartial.cshtml", productUpdateVM);
        }

        var productUpdateDTO = productUpdateVM.Adapt<ProductUpdateDTO>();
        var productExisting = await _productService.GetByIdAsync(productUpdateVM.Id);

        if (productUpdateVM.NewImage != null && productUpdateVM.NewImage.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await productUpdateVM.NewImage.CopyToAsync(memoryStream);
                productUpdateDTO.Image = memoryStream.ToArray();
            }
        }
        else
        {
            productUpdateDTO.Image = productExisting.Data.Image;
        }

        productUpdateDTO.IngredientsId = productUpdateVM.Ingredients.Where(m => m.Selected).Select(m => new ProductIngredientDTO
        {
            IngredientsId = m.IngredientId,
            IsOptional = m.IsOptional
        }).ToList();

        var result = await _productService.UpdateAsync(productUpdateDTO);
        if (!result.IsSuccess)
        {
            NotifyError(_stringLocalizer["Update failed!"]);
            productUpdateVM.Categories = await GetCategory(productUpdateVM.CategoryId);
            productUpdateVM.Ingredients = await GetIngredient(productUpdateVM.Ingredients.Select(x => x.IngredientId).ToList());
            return PartialView("/Areas/Admin/Views/AdminProduct/Partials/_UpdatePartial.cshtml", productUpdateVM);
        }
        Notify(_stringLocalizer["Update Success!"], notificationType: UI.Models.NotificationType.success);
        return RedirectToAction("Index");
    }

    private async Task<SelectList> GetCategory(Guid? categoryId = null)
    {
        var categories = (await _categoryService.GetAllAsync()).Data;
        return new SelectList(categories.Select(src => new SelectListItem
        {
            Value = src.Id.ToString(),
            Text = src.Name,
            Selected = src.Id == (categoryId != null ? categoryId.Value : categoryId)
        }), "Value", "Text");
    }

    private async Task<List<AdminIngredientSelectionVM>> GetIngredient(List<Guid>? selectedIngredientIds = null, List<Guid>? optionalIngredientIds = null)
    {
        var ingredientResult = await _ingredientService.GetAllAsync();
        var ingredients = ingredientResult.Data ?? new List<IngredientListDTO>();

        return ingredients.Select(m => new AdminIngredientSelectionVM
        {
            IngredientId = m.Id,
            IngredientName = m.Name,
            Selected = selectedIngredientIds != null && selectedIngredientIds.Contains(m.Id),
            IsOptional = optionalIngredientIds != null && optionalIngredientIds.Contains(m.Id)

        }).OrderBy(x => x.IngredientName).ToList();



    }
    private async Task<List<AdminIngredientSelectionVM>> GetIngredient()
    {
        var ingredientResult = await _ingredientService.GetAllAsync();
        var ingredients = ingredientResult.Data ?? new List<IngredientListDTO>();

        return ingredients.Select(m => new AdminIngredientSelectionVM
        {
            IngredientId = m.Id,
            IngredientName = m.Name,
            Selected = false,
            IsOptional = false

        }).OrderBy(x => x.IngredientName).ToList();
    }
    public async Task<IActionResult> Delete(Guid id)
    {

        var result = await _productService.DeleteAsync(id);
        if (!result.IsSuccess)
        {
            Notify(_stringLocalizer["Delete failed!"], notificationType: UI.Models.NotificationType.error);
            return RedirectToAction("Index");
        }

        Notify(_stringLocalizer["Success"], notificationType: UI.Models.NotificationType.success);
        return RedirectToAction("Index");
    }

}

