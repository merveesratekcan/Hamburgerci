using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Core.BaseEntityConfigurations;
using RestaurantApp.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Configurations.OrdersConfigurations;

public class CartProductConfiguration : AuditableEntityConfiguration<CartProduct>
{
    public override void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.Property(x=>x.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
        base.Configure(builder);
    }
}
