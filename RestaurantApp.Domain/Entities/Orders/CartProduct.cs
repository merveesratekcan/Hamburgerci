using RestaurantApp.Domain.Core.BaseEntities;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Orders;

public class CartProduct : AuditableEntity
{
    public decimal TotalPrice { get; set; }
    public int ProductCount { get; set; }

    //Nav Prop
    public Guid CartId { get; set; }
    public virtual Cart Carts { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Products { get; set; }
}
