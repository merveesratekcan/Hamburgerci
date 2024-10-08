using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantApp.Domain.Enums;

namespace RestaurantApp.UI.Areas.Admin.Models.CampaignVMs;

public class AdminCampaignUpdateVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Discount { get; set; }
    public IFormFile? NewImage { get; set; }
    public string? ExistingImage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    //public CampaignStatus CampaignStatus { get; set; }

    public List<Guid> SelectedProducts { get; set; } = new List<Guid>();
    public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();

}
