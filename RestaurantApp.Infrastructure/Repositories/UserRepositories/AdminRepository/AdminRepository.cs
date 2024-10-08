using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.UserContracts.AdminRepositories;
using RestaurantApp.Domain.Entities.Users;
using RestaurantApp.Infrastructure.Context;

namespace RestaurantApp.Infrastructure.Repositories.UserRepositories.AdminRepository;

public class AdminRepository : EFBaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<Admin?> GetByIdentityId(string identityId)
    {
        return _table.FirstOrDefault(x => x.IdentityId == identityId);
    }
}
