using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.WebsitesContracts.SocialMediaRepositories;
using RestaurantApp.Domain.Entities.Websites;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.WebsitesRepositories.SocialMediaRepository;

public class SocialMediaRepository : EFBaseRepository<SocialMedia>, ISocialMediaRepository
{
    public SocialMediaRepository(AppDbContext context) : base(context)
    {
    }
}
