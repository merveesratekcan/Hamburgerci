using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Products;

public class Ingredient : AuditableEntity
{
    public string Name { get; set; }

    //nav prop
    public virtual IEnumerable<ProductIngredient> ProductIngredients { get; set; }
    public Ingredient()
    {
        ProductIngredients = new HashSet<ProductIngredient>();
    }
}
