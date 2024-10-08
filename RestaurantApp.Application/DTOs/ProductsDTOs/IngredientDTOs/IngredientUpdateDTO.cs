namespace RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;

public class IngredientUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    //ProductIngredient
    public bool IsOptional { get; set; }
}
