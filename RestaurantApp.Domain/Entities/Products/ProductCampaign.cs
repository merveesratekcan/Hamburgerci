using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Products;

public class ProductCampaign:AuditableEntity
{
    public virtual Product Products { get; set; }
    public Guid ProductId { get; set; }
    public virtual Campaign Campaigns { get; set; }   
    public Guid CampaignId { get; set; }
}
