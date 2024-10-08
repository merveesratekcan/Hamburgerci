using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Domain.Entities.Users;
using RestaurantApp.Domain.Contracts.Interfaces;

namespace RestaurantApp.Domain.Contracts.UserContracts.AdminRepositories;

public interface IAdminRepository : 
                                        IAsyncRepository, 
                                        IAsyncInsertableRepository<Admin>, 
                                        IAsyncFindableRepository<Admin>,
                                        IAsyncQueryableRepository<Admin>, 
                                        IAsyncUpdatableRepository<Admin>, 
                                        IAsyncDeletableRepository<Admin>, 
                                        IAsyncTransactionRepository
{
    Task<Admin?> GetByIdentityId(string identityId);
}


