using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Core.BaseEntityConfigurations;
using RestaurantApp.Domain.Entities.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Configurations.WebsitesConfigurations;

public class SliderConfiguration : AuditableEntityConfiguration<Slider>
{
    public override void Configure(EntityTypeBuilder<Slider> builder)
    {
        base.Configure(builder);
    }
}
