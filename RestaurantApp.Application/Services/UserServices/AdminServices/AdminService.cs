using Mapster;
using RestaurantApp.Domain.Enums;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Entities.Users;
using RestaurantApp.Domain.Contracts.UserContracts.AdminRepositories;
using RestaurantApp.Application.DTOs.UsersDTOs.AdminDTOs;
using RestaurantApp.Application.Services.UserServices.AccountServices;

namespace RestaurantApp.Application.Services.UserServices.AdminServices;

public class AdminService : IAdminService
{
    private readonly IAccountService _accountService;
    private readonly IAdminRepository _adminRepository;
    private readonly UserManager<IdentityUser> _userManager;

    public AdminService(IAccountService accountService, IAdminRepository adminRepository, UserManager<IdentityUser> userManager)
    {
        _accountService = accountService;
        _adminRepository = adminRepository;
        _userManager = userManager;
    }

    public async Task<IDataResult<AdminDTO>> CreateAsync(AdminCreateDTO adminCreateDTO)
    {
        if (await _accountService.AnyAsync(x => x.Email == adminCreateDTO.Email))
        {
            return new ErrorDataResult<AdminDTO>("Bu email adresi kullanılmaktadır.");
        }
        IdentityUser identityUser = new()
        {
            Email = adminCreateDTO.Email,
            NormalizedEmail = adminCreateDTO.Email.ToUpperInvariant(),
            UserName = adminCreateDTO.Email,
            NormalizedUserName = adminCreateDTO.Email.ToUpperInvariant(),
            EmailConfirmed = true,
        };
        DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>("Admin oluşturulamadı.");

        var strategy = await _adminRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _adminRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.CreateUserAsync(identityUser, Roles.Admin);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AdminDTO>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }
                var admin = adminCreateDTO.Adapt<Admin>();
                admin.IdentityId = identityUser.Id;
                await _adminRepository.AddAsync(admin);
                await _adminRepository.SaveChangesAsync();
                result = new SuccessDataResult<AdminDTO>("Kullanıcı ekleme başarılı");
                transactionScope.Commit();

            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AdminDTO>("Ekleme Başarısız" + ex.Message);
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
        var deletingAdmin = await _adminRepository.GetByIdAsync(id);
        if (deletingAdmin == null)
        {
            return new ErrorResult("Admin bulunamadı.");
        }

        await _adminRepository.DeleteAsync(deletingAdmin);
        await _adminRepository.SaveChangesAsync();
        return new SuccessResult("Admin başarıyla silindi.");
    }

    public async Task<Guid> GetAdminIdByIdentityId(string identityId)
    {
        var admin = await _adminRepository.GetByIdentityId(identityId);
        if (admin == null)
        {
            return Guid.Empty;
        }
        return admin.Id;
    }

    public async Task<IDataResult<List<AdminListDTO>>> GetAllAsync()
    {
        var admins = await _adminRepository.GetAllAsync();
        var adminListDTO = admins.Adapt<List<AdminListDTO>>();
        if (admins.Count() <= 0)
        {
            return new ErrorDataResult<List<AdminListDTO>>(adminListDTO, "Admin bulunamadı.");
        }
        return new SuccessDataResult<List<AdminListDTO>>(adminListDTO, "Adminler listelendi.");
    }



    public async Task<IDataResult<AdminDTO>> GetByIdAsync(Guid id)
    {
        var admin = await _adminRepository.GetByIdAsync(id);
        if (admin == null)
        {
            return new ErrorDataResult<AdminDTO>("Admin bulunamadı.");
        }

        var adminDto = admin.Adapt<AdminDTO>();
        return new SuccessDataResult<AdminDTO>(adminDto, "Admin başarıyla bulundu.");
    }

    public async Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO)
    {
        DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>();
        var strategy = await _adminRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _adminRepository.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                // Güncellenecek Admin kullanıcıyı ID ile getir
                var updatingUser = await _adminRepository.GetByIdAsync(adminUpdateDTO.Id, false);
                if (updatingUser == null)
                {
                    result = new ErrorDataResult<AdminDTO>("Güncellenecek Kullanıcı Bulunamadı");
                    transactionScope.Rollback();
                    return;
                }

                // IdentityUser kaydını getir
                var identityUser = await _accountService.FindByIdAsync(updatingUser.IdentityId);
                if (identityUser == null)
                {
                    result = new ErrorDataResult<AdminDTO>("Güncellenecek Kullanıcıya ait Identity kaydı bulunamadı");
                    transactionScope.Rollback();
                    return;
                }
                // IdentityUser bilgilerini güncelle
                if (!(updatingUser.Email == adminUpdateDTO.Email))
                {
                    identityUser.Email = adminUpdateDTO.Email;
                    identityUser.UserName = adminUpdateDTO.Email;
                    identityUser.NormalizedEmail = adminUpdateDTO.Email.ToUpperInvariant();
                    identityUser.NormalizedUserName = adminUpdateDTO.Email.ToUpperInvariant();
                }
                // Admin bilgilerini DTO'dan güncelle
                updatingUser = adminUpdateDTO.Adapt(updatingUser);
                await _adminRepository.UpdateAsync(updatingUser);

                var identityResult = await _accountService.UpdateUserAsync(identityUser);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AdminDTO>("Identity bilgilerini güncelleme başarısız: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                    transactionScope.Rollback();
                    return;
                }

                // Değişiklikleri kaydet
                await _adminRepository.SaveChangesAsync();
                result = new SuccessDataResult<AdminDTO>(updatingUser.Adapt<AdminDTO>(), "Kullanıcı Güncelleme Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AdminDTO>("Güncelleme Başarısız: " + ex.Message);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });
        return result;
    }
    public async Task<IdentityResult> ChangeAdminPasswordAsync(AdminChangePasswordDTO model)
    {
        var admin = await _adminRepository.GetByIdAsync(model.Id);
        if (admin == null)
        {
            throw new Exception();
        }
        var user = await _accountService.FindByIdAsync(admin.IdentityId);
        return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
    }

    public async Task<IDataResult<AdminDTO>> GetAdminProfileByEmailAsync(string email)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        if (identityUser == null)
        {
            return new ErrorDataResult<AdminDTO>("Admin bulunamadı.");
        }

        var admin = await _adminRepository.GetByIdentityId(identityUser.Id);
        if (admin == null)
        {
            return new ErrorDataResult<AdminDTO>("Admin bulunamadı.");
        }

        var adminDto = admin.Adapt<AdminDTO>();
        return new SuccessDataResult<AdminDTO>(adminDto, "Admin başarıyla bulundu.");
    }
}
