using Microsoft.AspNetCore.Identity;
using RestaurantApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.UserServices.AccountServices;

public interface IAccountService
{
    Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression);
    Task<IdentityUser?> FindByIdAsync(string identityId);
    Task<IdentityResult> CreateUserAsync(IdentityUser user, Roles role);
    Task<IdentityResult> DeleteUserAsync(string identityId);
    Task<Guid> GetUserIdAsync(string identityId, string role);
    Task<IdentityResult> UpdateUserAsync(IdentityUser user);
    Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser identityUser);
}
