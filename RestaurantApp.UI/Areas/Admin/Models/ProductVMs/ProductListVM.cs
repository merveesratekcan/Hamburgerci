using RestaurantApp.UI.Areas.Admin.Models.IngredientVMs;

namespace RestaurantApp.UI.Areas.Admin.Models.ProductVMs;

public class ProductListVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public byte[]? Image { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<AdminIngredientSelectionVM> Ingredients { get; set; }

    public ProductListVM()
    {
        Ingredients = new List<AdminIngredientSelectionVM>();
    }

}
