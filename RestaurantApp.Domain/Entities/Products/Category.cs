using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Products;

public class Category : AuditableEntity
{
    public string Name { get; set; }

    //nav prop
    public virtual IEnumerable<Product> Products { get; set; }
    public Category()
    {
        Products = new HashSet<Product>();
    }
}
