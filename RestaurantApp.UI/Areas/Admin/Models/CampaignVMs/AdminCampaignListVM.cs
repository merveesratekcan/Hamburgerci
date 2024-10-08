using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Enums;

namespace RestaurantApp.UI.Areas.Admin.Models.CampaignVMs;

public class AdminCampaignListVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Discount { get; set; }
    public byte[]? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    //public CampaignStatus Status { get; set; }

    //public string ProductName { get; set; }

    //public List<Guid> SelectedProducts { get; set; }
    public List<string> ProductNames { get; set; }
    public List<decimal> OriginalPrices { get; set; }
    public List<decimal> DiscountedPrices
    {
        get
        {
            return OriginalPrices?.Select(price => price * (1 - Discount / 100)).ToList();
        }
    }

}
