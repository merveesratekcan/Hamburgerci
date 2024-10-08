using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Users.AppUser
{
    public class UserAddress : AuditableEntity
    {
        public Guid Id { get; set; }
        public string? Address { get; set; }

        //Nav prop
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
