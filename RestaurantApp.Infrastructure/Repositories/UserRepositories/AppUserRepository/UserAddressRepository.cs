using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.UserContracts.AppUserRepositories;
using RestaurantApp.Domain.Entities.Users.AppUser;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.UserRepositories.AppUserRepository;

public class UserAddressRepository : EFBaseRepository<UserAddress>, IUserAddressRepository
{
    public UserAddressRepository(AppDbContext context) : base(context)
    {
    }
}
