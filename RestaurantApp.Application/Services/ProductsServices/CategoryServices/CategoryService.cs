using Mapster;
using RestaurantApp.Application.DTOs.ProductsDTOs.CategoryDTOs;
using RestaurantApp.Domain.Contracts.ProductContracts.CategoryRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.ProductsServices.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IDataResult<CategoryDTO>> AddAsync(CategoryCreateDTO categoryCreateDTO)
    {
        if (await _categoryRepository.AnyAsync(x => x.Name.ToLower() == categoryCreateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<CategoryDTO>("Category already exists");
        }
        var newCategory = categoryCreateDTO.Adapt<Category>();
        await _categoryRepository.AddAsync(newCategory);
        await _categoryRepository.SaveChangesAsync();
        return new SuccessDataResult<CategoryDTO>(newCategory.Adapt<CategoryDTO>(), "Category Add success!");
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return new ErrorResult("Category not found");
        }
        await _categoryRepository.DeleteAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return new SuccessResult("Category delete success!");
    }

    public async Task<IDataResult<List<CategoryListDTO>>> GetAllAsync()
    {
        var category = await _categoryRepository.GetAllAsync();
        if (category.Count() <= 0)
        {
            return new ErrorDataResult<List<CategoryListDTO>>(category.Adapt<List<CategoryListDTO>>(), "Konu Sistemde Kayıtlı");
        }
        return new SuccessDataResult<List<CategoryListDTO>>(category.Adapt<List<CategoryListDTO>>(), "Category List success!");
    }

    public async Task<IDataResult<CategoryDTO>> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return new ErrorDataResult<CategoryDTO>("Category not found");
        }
        return new SuccessDataResult<CategoryDTO>(category.Adapt<CategoryDTO>(), "Category get success!");
    }

    public Task<IDataResult<List<CategoryDTO>>> GetCategoriesByProductIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<IDataResult<CategoryDTO>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryUpdateDTO.Id);
        if (category is null)
        {
            return new ErrorDataResult<CategoryDTO>("Category not found");
        }
        if (await _categoryRepository.AnyAsync(x => x.Name.ToLower() == categoryUpdateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<CategoryDTO>("Category already exists");
        }
        category.Name = categoryUpdateDTO.Name;
        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return new SuccessDataResult<CategoryDTO>(category.Adapt<CategoryDTO>(), "Category update success!");
    }

}
