using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using RestaurantApp.Application.Services.ProductsServices.CategoryServices;
using RestaurantApp.Application.Services.ProductsServices.ProductIngredientServices;
using RestaurantApp.Application.Services.ProductsServices.ProductServices;
using RestaurantApp.UI.Areas.Admin.Models.IngredientVMs;
using RestaurantApp.UI.Areas.Admin.Models.ProductIngredientVMs;
using RestaurantApp.UI.Areas.Admin.Models.ProductVMs;
using RestaurantApp.UI.Models.Order;

namespace RestaurantApp.UI.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductIngredientService _productIngredientService;
        private readonly IStringLocalizer<ProductController> _stringLocalizer;

        public ProductController(IProductService productService, ICategoryService categoryService, IStringLocalizer<ProductController> stringLocalizer, IProductIngredientService productIngredientService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _stringLocalizer = stringLocalizer;
            _productIngredientService = productIngredientService;
        }

        public async Task<IActionResult> Index()
        {

            var result = await _productService.GetAllAsync();
            if (!result.IsSuccess)
            {
                Notify(_stringLocalizer["Product not found!"], notificationType: UI.Models.NotificationType.error);
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

            Notify(_stringLocalizer["Product List successfully!"], notificationType: UI.Models.NotificationType.success);
            return View(productVms);
        }
        public async Task<IActionResult> AddToCart(Guid id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            var addProductToCartVM = result.Data.Adapt<AddProductToCartVM>();

            #region İlgili Producta Ait Ingredient'ları Çekme İşlemi
            //var productIngredients = await _productIngredientService.GetAllAsync();
            //var productIngredientVM = productIngredients.Data.Adapt<List<ProductIngredientListVM>>();
            //var ingredients = productIngredientVM.Where(p => p.ProductId == id).ToList();
            #endregion
            var ingredients = await _productService.GetIngredientsByProductIdAsync(id);
            //addProductToCartVM.Ingredients = ingredients.Adapt<List<IngredientSelectionVM>>();
            //foreach (var ingredient in addProductToCartVM.Ingredients)
            //{
            //    ingredient.IngredientId = 
            //}

            // addToCartVM'e dönüştür
            return View();

        }
     

    }
}
