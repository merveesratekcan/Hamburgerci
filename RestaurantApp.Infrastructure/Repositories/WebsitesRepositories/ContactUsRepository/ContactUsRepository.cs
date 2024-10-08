using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using RestaurantApp.Domain.Contracts.WebsitesContracts.ContactUsRepositories;
using RestaurantApp.Domain.Entities.Websites;
using RestaurantApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Repositories.WebsitesRepositories.ContactUsRepository;

public class ContactUsRepository : EFBaseRepository<ContactUs>, IContactUsRepository
{
    public ContactUsRepository(AppDbContext context) : base(context)
    {
    }
}
