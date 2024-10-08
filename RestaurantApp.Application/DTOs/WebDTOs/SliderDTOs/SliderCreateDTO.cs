using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.WebDTOs.SliderDTOs;

public class SliderCreateDTO
{
    public string Title1 { get; set; }
    public string? Title2 { get; set; }
    public string? Title3 { get; set; }

    public string? Description1 { get; set; }
    public string? Description2 { get; set; }
    public string? Description3 { get; set; }
}


