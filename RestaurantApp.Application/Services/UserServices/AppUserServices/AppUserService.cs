using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;
using RestaurantApp.Application.DTOs.UsersDTOs.AppUserDTOs;
using RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;
using RestaurantApp.Application.Services.UserServices.AccountServices;
using RestaurantApp.Application.Services.UserServices.UserAddressServices;
using RestaurantApp.Domain.Contracts.UserContracts.AppUserRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Entities.Users.AppUser;
using RestaurantApp.Domain.Enums;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;
using System.Runtime.Intrinsics.X86;

namespace RestaurantApp.Application.Services.UserServices.AppUserServices;

public class AppUserService : IAppUserService
{
    private readonly IAccountService _accountService;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserAddressRepository _userAddressRepository;
    private readonly IUserAddressService _userAddressService;
    private readonly UserManager<IdentityUser> _userManager;

    public AppUserService(IAccountService accountService, IAppUserRepository appUserRepository, UserManager<IdentityUser> userManager, IUserAddressRepository userAddressRepository, IUserAddressService userAddressService)
    {
        _accountService = accountService;
        _appUserRepository = appUserRepository;
        _userManager = userManager;
        _userAddressRepository = userAddressRepository;
        _userAddressService = userAddressService;
    }

    public async Task<IDataResult<AppUserDTO>> CreateAsync(AppUserCreateDTO appUserCreateDTO)
    {
        if (await _accountService.AnyAsync(x => x.Email == appUserCreateDTO.Email))
        {
            return new ErrorDataResult<AppUserDTO>("Bu email adresi kullanılmaktadır.");
        }
        IdentityUser identityUser = new()
        {
            Email = appUserCreateDTO.Email,
            NormalizedEmail = appUserCreateDTO.Email.ToUpperInvariant(),
            UserName = appUserCreateDTO.Email,
            NormalizedUserName = appUserCreateDTO.Email.ToUpperInvariant(),
            EmailConfirmed = true,
        };
        DataResult<AppUserDTO> result = new ErrorDataResult<AppUserDTO>("AppUser oluşturulamadı.");

        var strategy = await _appUserRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _appUserRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.CreateUserAsync(identityUser, Roles.AppUser);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AppUserDTO>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }
                var appUser = appUserCreateDTO.Adapt<AppUser>();
                appUser.IdentityId = identityUser.Id;
                // Adresleri AppUser'a ekleyin
                if (appUserCreateDTO.Addresses != null)
                {
                    appUser.Addresses = appUserCreateDTO.Addresses.Select(dto => new UserAddress
                    {
                        Address = dto.Address,
                        AppUserId = appUser.Id
                    }).ToList();
                }
                await _appUserRepository.AddAsync(appUser);
                await _appUserRepository.SaveChangesAsync();
                var appUserDTO = appUser.Adapt<AppUserDTO>();
                result = new SuccessDataResult<AppUserDTO>(appUserDTO,"Kullanıcı ekleme başarılı");
                transactionScope.Commit();

            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AppUserDTO>("Ekleme Başarısız" + ex.Message);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });
        return result;
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var deletingAppUser = await _appUserRepository.GetByIdAsync(id);
        if (deletingAppUser == null)
        {
            return new ErrorResult("AppUser bulunamadı.");
        }

        await _appUserRepository.DeleteAsync(deletingAppUser);
        await _appUserRepository.SaveChangesAsync();
        return new SuccessResult("AppUser başarıyla silindi.");
    }

    public async Task<Guid> GetAppUserIdByIdentityId(string identityId)
    {
        var appUser = await _appUserRepository.GetByIdentityId(identityId);
        if (appUser == null)
        {
            return Guid.Empty;
        }
        return appUser.Id;
    }

    public async Task<IDataResult<List<AppUserListDTO>>> GetAllAsync()
    {
        var appUsers = await _appUserRepository.GetAllAsync();
        foreach (var appUser in appUsers)
        {
            var addresses = await _userAddressRepository.GetAllAsync(x => x.AppUserId == appUser.Id);
            appUser.Addresses = addresses;
        }

        var appUsersListDTO = appUsers.Select(appUser => new AppUserListDTO
        {
            Id = appUser.Id,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Email = appUser.Email,
            ContactNumber = appUser.ContactNumber,
            Addresses = appUser.Addresses.Select(a => a.Address).ToList()
        }).ToList();

        if (appUsers.Count() <= 0)
        {
            return new ErrorDataResult<List<AppUserListDTO>>(appUsersListDTO, "AppUser bulunamadı.");
        }
        return new SuccessDataResult<List<AppUserListDTO>>(appUsersListDTO, "AppUserler listelendi.");
    }



    public async Task<IDataResult<AppUserDTO>> GetByIdAsync(Guid id)
    {
        var appUser = await _appUserRepository.GetByIdAsync(id);
        if (appUser == null)
        {
            return new ErrorDataResult<AppUserDTO>("AppUser bulunamadı.");
        }

        var appUserDto = appUser.Adapt<AppUserDTO>();
        return new SuccessDataResult<AppUserDTO>(appUserDto, "AppUser başarıyla bulundu.");
    }

    public async Task<IDataResult<AppUserDTO>> UpdateAsync(AppUserUpdateDTO appUserUpdateDTO)
    {
        DataResult<AppUserDTO> result = new ErrorDataResult<AppUserDTO>();
        var strategy = await _appUserRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _appUserRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                // Güncellenecek AppUser kullanıcıyı ID ile getir
                var updatingUser = await _appUserRepository.GetByIdAsync(appUserUpdateDTO.Id, false);
                if (updatingUser == null)
                {
                    result = new ErrorDataResult<AppUserDTO>("Güncellenecek Kullanıcı Bulunamadı");
                    transactionScope.Rollback();
                    return;
                }

                // IdentityUser kaydını getir
                var identityUser = await _accountService.FindByIdAsync(updatingUser.IdentityId);
                if (identityUser == null)
                {
                    result = new ErrorDataResult<AppUserDTO>("Güncellenecek Kullanıcıya ait Identity kaydı bulunamadı");
                    transactionScope.Rollback();
                    return;
                }
                // IdentityUser bilgilerini güncelle
                if (!(updatingUser.Email == appUserUpdateDTO.Email))
                {
                    identityUser.Email = appUserUpdateDTO.Email;
                    identityUser.UserName = appUserUpdateDTO.Email;
                    identityUser.NormalizedEmail = appUserUpdateDTO.Email.ToUpperInvariant();
                    identityUser.NormalizedUserName = appUserUpdateDTO.Email.ToUpperInvariant();
                }
                // AppUser bilgilerini DTO'dan güncelle
                updatingUser = appUserUpdateDTO.Adapt(updatingUser);
                await _appUserRepository.UpdateAsync(updatingUser);

                var identityResult = await _accountService.UpdateUserAsync(identityUser);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AppUserDTO>("Identity bilgilerini güncelleme başarısız: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                    transactionScope.Rollback();
                    return;
                }

                // Değişiklikleri kaydet
                await _appUserRepository.SaveChangesAsync();
                result = new SuccessDataResult<AppUserDTO>(updatingUser.Adapt<AppUserDTO>(), "Kullanıcı Güncelleme Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AppUserDTO>("Güncelleme Başarısız: " + ex.Message);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });
        return result;
    }
    public async Task<IdentityResult> ChangeAppUserPasswordAsync(AppUserChangePasswordDTO model)
    {
        var appUser = await _appUserRepository.GetByIdAsync(model.Id);
        if (appUser == null)
        {
            throw new Exception();
        }
        var user = await _accountService.FindByIdAsync(appUser.IdentityId);
        return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
    }

    public async Task<IDataResult<AppUserDTO>> GetAppUserProfileByEmailAsync(string email)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        if (identityUser == null)
        {
            return new ErrorDataResult<AppUserDTO>("AppUser bulunamadı.");
        }

        var appUser = await _appUserRepository.GetByIdentityId(identityUser.Id);
        if (appUser == null)
        {
            return new ErrorDataResult<AppUserDTO>("AppUser bulunamadı.");
        }

        var appUserDto = appUser.Adapt<AppUserDTO>();
        return new SuccessDataResult<AppUserDTO>(appUserDto, "AppUser başarıyla bulundu.");
    }
}
