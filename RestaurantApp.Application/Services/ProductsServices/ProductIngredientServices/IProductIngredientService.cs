using RestaurantApp.Application.DTOs.ProductMaterialDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductIngredientDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.ProductsServices.ProductIngredientServices;

public interface IProductIngredientService
{
    Task<IDataResult<ProductIngredientDTO>> AddAsync(ProductIngredientCreateDTO productIngredientCreateDTO);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<ProductIngredientDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<List<ProductIngredientListDTO>>> GetAllAsync();
    Task<IDataResult<ProductIngredientDTO>> UpdateAsync(ProductIngredientUpdateDTO productIngredientUpdateDTO);

}
