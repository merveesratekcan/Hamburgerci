using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.WebDTOs.FeatureDTOs;

public class FeatureCreateDTO
{

    public string FeatureTitle { get; set; }
    public string? FeatureDescription { get; set; }
    public bool FeatureStatus { get; set; }
}
