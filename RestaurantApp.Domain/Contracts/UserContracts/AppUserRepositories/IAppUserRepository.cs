using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Users.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.UserContracts.AppUserRepositories;

public interface IAppUserRepository :
                                             IAsyncRepository,
                                             IAsyncInsertableRepository<AppUser>,
                                             IAsyncFindableRepository<AppUser>,
                                             IAsyncQueryableRepository<AppUser>,
                                             IAsyncUpdatableRepository<AppUser>,
                                             IAsyncDeletableRepository<AppUser>,
                                             IAsyncTransactionRepository
{
    Task<AppUser?> GetByIdentityId(string identityId);

}
