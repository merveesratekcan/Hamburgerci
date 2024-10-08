using RestaurantApp.Domain.Core.BaseEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Core.BaseEntityConfigurations
{
    public class BaseUserConfiguration<TEntity>:AuditableEntityConfiguration<TEntity> where TEntity : BaseUser
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(128);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(128);
            base.Configure(builder);
        }
    }
}
