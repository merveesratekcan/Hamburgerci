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

public class ProductIngredientConfiguration : AuditableEntityConfiguration<ProductIngredient>
{
    public override void Configure(EntityTypeBuilder<ProductIngredient> builder)
    {
        builder.Property(x => x.IsOptional);
        builder.Property(x => x.IngredientPrice).HasColumnType("decimal(18,2)").IsRequired(false);
        base.Configure(builder);
    }
}
