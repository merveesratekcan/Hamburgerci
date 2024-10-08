using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.ProductRepositories.ProductRepository;

public class ProductRepository : EFBaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {

    }
}
