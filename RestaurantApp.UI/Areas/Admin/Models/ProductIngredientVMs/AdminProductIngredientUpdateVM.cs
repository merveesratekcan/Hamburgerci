using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantApp.UI.Areas.Admin.Models.ProductIngredientVMs;

public class AdminProductIngredientUpdateVM
{
    public Guid Id { get; set; }
    public bool IsOptional { get; set; }
    public decimal? IngredientPrice { get; set; }
    public Guid IngredientsId { get; set; }
    public SelectList? Ingredients { get; set; }

    public SelectList? Products { get; set; }

    public Guid ProductId { get; set; }   

}
