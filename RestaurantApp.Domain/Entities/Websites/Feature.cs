using RestaurantApp.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.Websites;

public class Feature : AuditableEntity
{
    public string FeatureTitle { get; set; }
    public string? FeatureDescription { get; set; }
    public bool FeatureStatus { get; set; } 
}
