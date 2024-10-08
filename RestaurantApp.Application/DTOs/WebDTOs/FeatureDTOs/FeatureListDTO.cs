using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.WebDTOs.FeatureDTOs;

public class FeatureListDTO
{
    public Guid Id { get; set; }
    public string FeatureTitle { get; set; }
    public string? FeatureDescription { get; set; }
    public bool FeatureStatus { get; set; }
}
