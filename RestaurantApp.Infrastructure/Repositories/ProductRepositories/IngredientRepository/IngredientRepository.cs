using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.ProductContracts.IngredientRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Infrastructure.Context;

namespace RestaurantApp.Infrastructure.Repositories.ProductRepositories.IngredientRepository;

public class IngredientRepository : EFBaseRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(AppDbContext context) : base(context)
    {

    }
}

