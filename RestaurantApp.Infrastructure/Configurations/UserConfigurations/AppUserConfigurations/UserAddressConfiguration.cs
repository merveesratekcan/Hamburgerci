using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Domain.Core.BaseEntityConfigurations;
using RestaurantApp.Domain.Entities.Users.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Configurations.UserConfigurations.AppUserConfigurations;

public class UserAddressConfiguration : AuditableEntityConfiguration<UserAddress>
{
    public override void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.Property(x => x.Address).HasMaxLength(512).IsRequired(false);
        base.Configure(builder);
    }
}
