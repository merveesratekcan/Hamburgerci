using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Core.BaseEntityConfigurations;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Configurations.ProductConfigurations;

public class CampaignConfiguration : AuditableEntityConfiguration<Campaign>
{
    public override void Configure(EntityTypeBuilder<Campaign> builder)
    {

        builder.Property(p => p.Name).HasMaxLength(128).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(512).IsRequired();
        builder.Property(p => p.StartDate).IsRequired();
        builder.Property(p => p.EndDate).IsRequired();
        builder.Property(p => p.Discount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(p => p.CampaignStatus);
        base.Configure(builder);
    }
}
