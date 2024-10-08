using RestaurantApp.Domain.Contracts.Interfaces;
using RestaurantApp.Domain.Entities.Users.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Contracts.UserContracts.AppUserRepositories;

public interface IUserAddressRepository :
                                             IAsyncRepository,
                                             IAsyncInsertableRepository<UserAddress>,
                                             IAsyncFindableRepository<UserAddress>,
                                             IAsyncQueryableRepository<UserAddress>,
                                             IAsyncUpdatableRepository<UserAddress>,
                                             IAsyncDeletableRepository<UserAddress>,
                                             IAsyncTransactionRepository
{

}

