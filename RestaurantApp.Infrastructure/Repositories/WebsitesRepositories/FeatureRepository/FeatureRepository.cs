using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.WebsitesContracts.FeatureRepositories;
using RestaurantApp.Domain.Entities.Websites;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.WebsitesRepositories.FeatureRepository;

public class FeatureRepository : EFBaseRepository<Feature>, IFeatureRepository
{
    public FeatureRepository(AppDbContext context) : base(context)
    {
    }
}
