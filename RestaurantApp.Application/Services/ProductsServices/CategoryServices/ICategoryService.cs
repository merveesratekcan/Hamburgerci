using RestaurantApp.Application.DTOs.ProductsDTOs.CategoryDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.ProductsServices.CategoryServices;

public interface ICategoryService
{
    Task<IDataResult<CategoryDTO>> AddAsync(CategoryCreateDTO categoryCreateDTO);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<CategoryDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<List<CategoryListDTO>>> GetAllAsync();
    Task<IDataResult<CategoryDTO>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO);    Task<IDataResult<List<CategoryDTO>>> GetCategoriesByProductIdAsync(Guid productId);


}
