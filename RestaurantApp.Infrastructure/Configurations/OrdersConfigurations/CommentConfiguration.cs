using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Core.BaseEntityConfigurations;
using RestaurantApp.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Configurations.OrdersConfigurations;

public class CommentConfiguration : AuditableEntityConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(x=>x.CommentName).HasMaxLength(128);
        builder.Property(x=>x.CommentTitle).HasMaxLength(128);
        builder.Property(x=>x.Comments).HasMaxLength(512);
        base.Configure(builder);
    }
}
