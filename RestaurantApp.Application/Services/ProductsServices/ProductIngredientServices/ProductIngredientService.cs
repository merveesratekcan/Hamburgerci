using Mapster;
using RestaurantApp.Application.DTOs.ProductMaterialDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductIngredientDTOs;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductIngredientRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.ProductsServices.ProductIngredientServices;

public class ProductIngredientService : IProductIngredientService
{
    private readonly IProductIngredientRepository _productIngredientRepository;

    public ProductIngredientService(IProductIngredientRepository productIngredientRepository)
    {
        _productIngredientRepository = productIngredientRepository;
    }

    public async Task<IDataResult<ProductIngredientDTO>> AddAsync(ProductIngredientCreateDTO productIngredientCreateDTO)
    {
        var newProductIngredient = productIngredientCreateDTO.Adapt<ProductIngredient>();
        await _productIngredientRepository.AddAsync(newProductIngredient);
        await _productIngredientRepository.SaveChangesAsync();
        return new SuccessDataResult<ProductIngredientDTO>(newProductIngredient.Adapt<ProductIngredientDTO>(), "ProductIngredient Add success!");
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var deletingProductIngredient = await _productIngredientRepository.GetByIdAsync(id);
        if (deletingProductIngredient is null)
        {
            return new ErrorResult("ProductIngredient not found");
        }
        await _productIngredientRepository.DeleteAsync(deletingProductIngredient);
        await _productIngredientRepository.SaveChangesAsync();
        return new SuccessResult("ProductIngredient delete success!");
    }

    public async Task<IDataResult<List<ProductIngredientListDTO>>> GetAllAsync()
    {
        var productIngredient = await _productIngredientRepository.GetAllAsync();
        if (productIngredient.Count() <= 0)
        {
            return new ErrorDataResult<List<ProductIngredientListDTO>>(productIngredient.Adapt<List<ProductIngredientListDTO>>(), "No ProductIngredient found");
        }
        return new SuccessDataResult<List<ProductIngredientListDTO>>(productIngredient.Adapt<List<ProductIngredientListDTO>>(), "ProductIngredient List success!");
    }

    public async Task<IDataResult<ProductIngredientDTO>> GetByIdAsync(Guid id)
    {
        var productIngredients = await _productIngredientRepository.GetByIdAsync(id);
        if (productIngredients is null)
        {
            return new ErrorDataResult<ProductIngredientDTO>("Ingredient not found");
        }
        return new SuccessDataResult<ProductIngredientDTO>(productIngredients.Adapt<ProductIngredientDTO>(), "Ingredient get success!");
    }

    public async Task<IDataResult<ProductIngredientDTO>> UpdateAsync(ProductIngredientUpdateDTO productIngredientUpdateDTO)
    {
        var updatingProductIngredient = await _productIngredientRepository.GetByIdAsync(productIngredientUpdateDTO.Id);
        if (updatingProductIngredient is null)
        {
            return new ErrorDataResult<ProductIngredientDTO>("ProductIngredient not found");
        }
        updatingProductIngredient.ProductId = productIngredientUpdateDTO.ProductId;
        updatingProductIngredient.IsOptional = productIngredientUpdateDTO.IsOptional;
        updatingProductIngredient.IngredientPrice = productIngredientUpdateDTO?.IngredientPrice;
        updatingProductIngredient.IngredientId = productIngredientUpdateDTO.IngredientsId;

        await _productIngredientRepository.UpdateAsync(updatingProductIngredient);
        await _productIngredientRepository.SaveChangesAsync();
        return new SuccessDataResult<ProductIngredientDTO>(updatingProductIngredient.Adapt<ProductIngredientDTO>(), "Ingredient update success!");
    }

}
