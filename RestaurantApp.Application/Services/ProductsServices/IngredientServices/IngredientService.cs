using Mapster;
using RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;
using RestaurantApp.Domain.Contracts.ProductContracts.IngredientRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;

namespace RestaurantApp.Application.Services.ProductsServices.IngredientServices;

public class IngredientService : IIngredientService
{
    private readonly IIngredientRepository _ingredientRepository;

    public IngredientService(IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository;
    }

    public async Task<IDataResult<IngredientDTO>> CreateAsync(IngredientCreateDTO ingredientCreateDTO)
    {
        if (await _ingredientRepository.AnyAsync(x => x.Name.ToLower() == ingredientCreateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<IngredientDTO>("Ingredient already exists");
        }
        var newIngredient = ingredientCreateDTO.Adapt<Ingredient>();
        await _ingredientRepository.AddAsync(newIngredient);
        await _ingredientRepository.SaveChangesAsync();
        return new SuccessDataResult<IngredientDTO>(newIngredient.Adapt<IngredientDTO>(), "Ingredient Add success!");
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(id);
        if (ingredient is null)
        {
            return new ErrorResult("Ingredient not found");
        }
        await _ingredientRepository.DeleteAsync(ingredient);
        await _ingredientRepository.SaveChangesAsync();
        return new SuccessResult("Ingredient delete success!");
    }

    public async Task<IDataResult<List<IngredientListDTO>>> GetAllAsync()
    {
        var ingredients = await _ingredientRepository.GetAllAsync();
        if (ingredients.Count() <= 0)
        {
            return new ErrorDataResult<List<IngredientListDTO>>(ingredients.Adapt<List<IngredientListDTO>>(), "No ingredients found");
        }
        return new SuccessDataResult<List<IngredientListDTO>>(ingredients.Adapt<List<IngredientListDTO>>(), "Ingredient List success!");
    }

    public async Task<IDataResult<IngredientDTO>> GetByIdAsync(Guid id)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(id);
        if (ingredient is null)
        {
            return new ErrorDataResult<IngredientDTO>("Ingredient not found");
        }
        return new SuccessDataResult<IngredientDTO>(ingredient.Adapt<IngredientDTO>(), "Ingredient get success!");
    }


    public async Task<IDataResult<IngredientDTO>> UpdateAsync(IngredientUpdateDTO ingredientUpdateDTO)
    {
        var ingredient = await _ingredientRepository.GetByIdAsync(ingredientUpdateDTO.Id);
        if (ingredient is null)
        {
            return new ErrorDataResult<IngredientDTO>("Ingredient not found");
        }
        if (await _ingredientRepository.AnyAsync(x => x.Name.ToLower() == ingredientUpdateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<IngredientDTO>("Ingredient already exists");
        }
        ingredient.Name = ingredientUpdateDTO.Name;
        await _ingredientRepository.UpdateAsync(ingredient);
        await _ingredientRepository.SaveChangesAsync();
        return new SuccessDataResult<IngredientDTO>(ingredient.Adapt<IngredientDTO>(), "Ingredient update success!");
    }
}
