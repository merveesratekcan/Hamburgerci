using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.ProductContracts.ProductCampaignRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.ProductRepositories.ProductCampaignRepository;

public class ProductCampaignRepository : EFBaseRepository<ProductCampaign>, IProductCampaignRepository
{
    public ProductCampaignRepository(AppDbContext context) : base(context)
    {

    }
}
