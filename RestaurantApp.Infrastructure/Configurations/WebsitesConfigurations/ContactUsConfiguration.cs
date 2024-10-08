using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Core.BaseEntityConfigurations;
using RestaurantApp.Domain.Entities.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Configurations.WebsitesConfigurations;

public class ContactUsConfiguration : AuditableEntityConfiguration<ContactUs>
{
    public override void Configure(EntityTypeBuilder<ContactUs> builder)
    {
        base.Configure(builder);
    }
}
