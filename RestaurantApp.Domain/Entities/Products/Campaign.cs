using RestaurantApp.Domain.Core.BaseEntities;
using RestaurantApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Products;

public class Campaign : AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Discount { get; set; }
    public byte[]? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public CampaignStatus CampaignStatus { get; set; }

    //nav prop
    public virtual IEnumerable<ProductCampaign> ProductCampaigns { get; set; }
    public Campaign()
    {
        ProductCampaigns = new HashSet<ProductCampaign>();
    }
}
