using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.WebsitesContracts.AboutUsRepositories;
using RestaurantApp.Domain.Entities.Websites;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.WebsitesRepositories.AboutUsRepository;

public class AboutUsRepository : EFBaseRepository<AboutUs>, IAboutUsRepository
{
    public AboutUsRepository(AppDbContext context) : base(context)
    {
    }
}
