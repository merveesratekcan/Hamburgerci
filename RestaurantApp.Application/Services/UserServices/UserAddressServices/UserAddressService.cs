using Mapster;
using RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;
using RestaurantApp.Domain.Contracts.UserContracts.AppUserRepositories;
using RestaurantApp.Domain.Entities.Users.AppUser;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;

namespace RestaurantApp.Application.Services.UserServices.UserAddressServices;

public class UserAddressService : IUserAddressService
{
    private readonly IUserAddressRepository _userAdressRepository;

    public UserAddressService(IUserAddressRepository userAdressRepository)
    {
        _userAdressRepository = userAdressRepository;
    }

    public async Task<IDataResult<UserAddressDTO>> AddAsync(UserAddressCreateDTO userAdressCreateDTO)
    {
        if (await _userAdressRepository.AnyAsync(x => x.Address.ToLower() == userAdressCreateDTO.Address.ToLower()))
        {
            return new ErrorDataResult<UserAddressDTO>("UserAddress already exists");
        }
        var newUserAddress = userAdressCreateDTO.Adapt<UserAddress>();
        await _userAdressRepository.AddAsync(newUserAddress);
        await _userAdressRepository.SaveChangesAsync();
        return new SuccessDataResult<UserAddressDTO>(newUserAddress.Adapt<UserAddressDTO>(), "UserAddress Add success!");
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var userAdress = await _userAdressRepository.GetByIdAsync(id);
        if (userAdress is null)
        {
            return new ErrorResult("UserAddress not found");
        }
        await _userAdressRepository.DeleteAsync(userAdress);
        await _userAdressRepository.SaveChangesAsync();
        return new SuccessResult("UserAddress delete success!");
    }

    public async Task<IDataResult<List<UserAddressListDTO>>> GetAllAsync()
    {
        var userAdress = await _userAdressRepository.GetAllAsync();
        if (userAdress.Count() <= 0)
        {
            return new ErrorDataResult<List<UserAddressListDTO>>(userAdress.Adapt<List<UserAddressListDTO>>(), "Konu Sistemde Kayıtlı");
        }
        return new SuccessDataResult<List<UserAddressListDTO>>(userAdress.Adapt<List<UserAddressListDTO>>(), "UserAddress List success!");
    }

    public async Task<IDataResult<UserAddressDTO>> GetByIdAsync(Guid id)
    {
        var userAdress = await _userAdressRepository.GetByIdAsync(id);
        if (userAdress is null)
        {
            return new ErrorDataResult<UserAddressDTO>("UserAddress not found");
        }
        return new SuccessDataResult<UserAddressDTO>(userAdress.Adapt<UserAddressDTO>(), "UserAddress get success!");
    }

    public async Task<IDataResult<UserAddressDTO>> UpdateAsync(UserAddressUpdateDTO userAdressUpdateDTO)
    {
        var userAdress = await _userAdressRepository.GetByIdAsync(userAdressUpdateDTO.Id);
        if (userAdress is null)
        {
            return new ErrorDataResult<UserAddressDTO>("UserAddress not found");
        }
        if (await _userAdressRepository.AnyAsync(x => x.Address.ToLower() == userAdressUpdateDTO.Address.ToLower()))
        {
            return new ErrorDataResult<UserAddressDTO>("UserAddress already exists");
        }
        userAdress.Address = userAdressUpdateDTO.Address;
        await _userAdressRepository.UpdateAsync(userAdress);
        await _userAdressRepository.SaveChangesAsync();
        return new SuccessDataResult<UserAddressDTO>(userAdress.Adapt<UserAddressDTO>(), "UserAddress update success!");
    }
}
