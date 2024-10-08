using RestaurantApp.Domain.Core.BaseEntities;
using RestaurantApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Orders;

public class Cart : AuditableEntity
{
    public int BasketProductCount { get; set; }
    public decimal BasketTotalPrice { get; set; }
    public string Description { get; set; }

    //Nav Prop
    public virtual Order Orders { get; set; }
    public virtual IEnumerable<CartProduct> CartProducts { get; set; }
    public Cart()
    {
        CartProducts = new HashSet<CartProduct>();
    }
}
