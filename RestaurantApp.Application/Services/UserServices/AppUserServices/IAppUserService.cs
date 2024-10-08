using Microsoft.AspNetCore.Identity;
using RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;
using RestaurantApp.Application.DTOs.UsersDTOs.AppUserDTOs;
using RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.UserServices.AppUserServices;

public interface IAppUserService
{
    Task<IDataResult<List<AppUserListDTO>>> GetAllAsync();
    Task<IDataResult<AppUserDTO>> CreateAsync(AppUserCreateDTO appUserCreateDTO);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<AppUserDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<AppUserDTO>> UpdateAsync(AppUserUpdateDTO appUserUpdateDTO);
    Task<IdentityResult> ChangeAppUserPasswordAsync(AppUserChangePasswordDTO model);
    Task<Guid> GetAppUserIdByIdentityId(string identityId);
    Task<IDataResult<AppUserDTO>> GetAppUserProfileByEmailAsync(string email);

}
