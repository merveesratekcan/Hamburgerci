using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApp.UI.Areas.Admin.Models.IngredientVMs;

namespace RestaurantApp.UI.Models.Order
{
    public class AddProductToCartVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ExistingImage { get; set; }
        public IFormFile? NewImage { get; set; }
        public List<IngredientSelectionVM> Ingredients { get; set; }

        public AddProductToCartVM()
        {
            Ingredients = new List<IngredientSelectionVM>();
        }

    }
}
