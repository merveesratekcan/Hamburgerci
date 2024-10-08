using RestaurantApp.Application.DTOs.ProductMaterialDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.ProductsDTOs.ProductDTOs;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public byte[]? Image { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<Guid> IngredientIds { get; set; }
    public List<string> IngredientNames { get; set; }
  
}
