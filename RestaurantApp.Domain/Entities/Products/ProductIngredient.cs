using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Products;

public class ProductIngredient:AuditableEntity
{

    public bool IsOptional { get; set; }
    public decimal? IngredientPrice { get; set; }

    //Nav Prop
    public virtual Product Products { get; set; }
    public Guid ProductId { get; set; }
    public virtual Ingredient Ingredients { get; set; }   
    public Guid IngredientId { get; set; }
}
