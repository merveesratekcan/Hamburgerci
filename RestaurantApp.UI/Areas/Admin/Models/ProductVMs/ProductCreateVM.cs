using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApp.UI.Areas.Admin.Models.IngredientVMs;

namespace RestaurantApp.UI.Areas.Admin.Models.ProductVMs;

public class ProductCreateVM
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IFormFile? NewImage { get; set; }
    public Guid CategoryId { get; set; }
    public SelectList? Categories { get; set; }
    public List<AdminIngredientSelectionVM> Ingredients { get; set; }

    public ProductCreateVM()
    {
        Ingredients = new List<AdminIngredientSelectionVM>();
    }
}
