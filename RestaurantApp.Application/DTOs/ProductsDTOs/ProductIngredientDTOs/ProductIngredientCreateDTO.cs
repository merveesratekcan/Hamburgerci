using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.ProductsDTOs.ProductIngredientDTOs;

public class ProductIngredientCreateDTO
{
    public bool IsOptional { get; set; }
    public decimal? IngredientPrice { get; set; }
    public Guid IngredientsId { get; set; }
    public string IngredientName { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
}
