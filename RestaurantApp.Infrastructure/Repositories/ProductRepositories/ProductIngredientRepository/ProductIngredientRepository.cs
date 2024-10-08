using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductIngredientRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Infrastructure.Context;

namespace RestaurantApp.Infrastructure.Repositories.ProductRepositories.ProductIngredientRepository;

public class ProductIngredientRepository : EFBaseRepository<ProductIngredient>, IProductIngredientRepository
{
    public ProductIngredientRepository(AppDbContext context) : base(context)
    {

    }
}

