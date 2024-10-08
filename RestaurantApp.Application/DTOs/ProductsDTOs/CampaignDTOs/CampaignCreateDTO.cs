using RestaurantApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.ProductsDTOs.CampaignDTOs;

public class CampaignCreateDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Discount { get; set; }
    public byte[]? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public CampaignStatus Status { get; set; }
    public List<Guid> ProductIds { get; set; }
    public List<string> ProductNames { get; set; }

}

