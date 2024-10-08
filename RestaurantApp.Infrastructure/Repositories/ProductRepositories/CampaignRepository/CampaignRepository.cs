using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.ProductContracts.CampaignRepositories;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.ProductRepositories.CampaignRepository;

public class CampaignRepository : EFBaseRepository<Campaign>, ICampaignRepository
{
    public CampaignRepository(AppDbContext context) : base(context)
    {

    }
}

