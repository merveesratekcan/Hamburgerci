using RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;

namespace RestaurantApp.Application.Services.ProductsServices.IngredientServices;

public interface IIngredientService
{
    Task<IDataResult<IngredientDTO>> CreateAsync(IngredientCreateDTO ingredientCreateDTO);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<IngredientDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<List<IngredientListDTO>>> GetAllAsync();
    Task<IDataResult<IngredientDTO>> UpdateAsync(IngredientUpdateDTO ingredientUpdateDTO);
}
