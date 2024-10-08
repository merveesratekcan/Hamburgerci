using RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;

namespace RestaurantApp.Application.Services.UserServices.UserAddressServices;

public interface IUserAddressService
{
    Task<IDataResult<UserAddressDTO>> AddAsync(UserAddressCreateDTO userAddressCreateDTO);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<UserAddressDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<List<UserAddressListDTO>>> GetAllAsync();
    Task<IDataResult<UserAddressDTO>> UpdateAsync(UserAddressUpdateDTO userAddressUpdateDTO);
}
