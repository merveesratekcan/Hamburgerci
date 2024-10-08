namespace RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;

public class IngredientListDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    //ProductIngredient
    public bool IsOptional { get; set; }
}
