using RestaurantApp.Domain.Core.BaseEntities;
using RestaurantApp.Domain.Entities.Users.AppUser;
using RestaurantApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Orders;

public class Order : AuditableEntity
{
    public string? OrderNote { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string OrderAddress { get; set; } = null!;
    public OrderStatus OrderStatus { get; set; }

    //Nav Prop
    public Guid AppUserId { get; set; }
    public virtual AppUser AppUsers { get; set; }
    public Guid CartId { get; set; }
    public virtual Cart Carts { get; set; }
}
