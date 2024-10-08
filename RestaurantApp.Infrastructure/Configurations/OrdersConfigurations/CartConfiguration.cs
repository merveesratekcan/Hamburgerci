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

public class CartConfiguration : AuditableEntityConfiguration<Cart>
{
    public override void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.Property(p => p.BasketTotalPrice).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(p => p.Description).HasMaxLength(128);
        builder.HasOne(cart => cart.Orders).WithOne(ord => ord.Carts).HasForeignKey<Order>(ord => ord.CartId);
        base.Configure(builder);
    }
}
