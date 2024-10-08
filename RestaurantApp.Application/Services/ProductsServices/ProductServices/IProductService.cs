using RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;

namespace RestaurantApp.Application.Services.ProductsServices.ProductServices;

public interface IProductService
{
    Task<IDataResult<List<ProductListDTO>>> GetAllAsync();
    Task<IDataResult<ProductDTO>> AddAsync(ProductCreateDTO productCreateDTO);
    Task<IDataResult<ProductDTO>> UpdateAsync(ProductUpdateDTO productUpdateDTO);
    Task<IDataResult<ProductDTO>> GetByIdAsync(Guid id);
    //Task<IDataResult<ProductDTO>> GetProductByCategoryNameIdAsync(Guid id);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<List<ProductListDTO>>> GetAllByProductIdAsync(Guid categoryId);
    Task<IDataResult<List<IngredientDTO>>> GetIngredientsByProductIdAsync(Guid productId);
 
}
