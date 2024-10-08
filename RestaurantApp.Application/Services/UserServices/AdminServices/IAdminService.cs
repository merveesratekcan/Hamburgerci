using Microsoft.AspNetCore.Identity;
using RestaurantApp.Application.DTOs.UsersDTOs.AdminDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.UserServices.AdminServices;

public interface IAdminService
{
    Task<IDataResult<List<AdminListDTO>>> GetAllAsync();
    Task<IDataResult<AdminDTO>> CreateAsync(AdminCreateDTO adminCreateDTO);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<AdminDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO);
    Task<IdentityResult> ChangeAdminPasswordAsync(AdminChangePasswordDTO model);
    Task<Guid> GetAdminIdByIdentityId(string identityId);
    Task<IDataResult<AdminDTO>> GetAdminProfileByEmailAsync(string email);
}
