using RestaurantApp.Application.DTOs.WebDTOs.AboutDTOs;
using RestaurantApp.Application.DTOs.WebDTOs.SliderDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.WebServices.SliderServices;

public interface ISliderService
{
    Task<IDataResult<List<SliderListDTO>>> GetAllAsync();
    Task<IDataResult<SliderDTO>> AddAsync(SliderCreateDTO sliderCreateDTO);
    Task<IDataResult<SliderDTO>> UpdateAsync(SliderUpdateDTO sliderUpdateDTO);
    Task<IDataResult<SliderDTO>> GetByIdAsync(Guid id);
    Task<IResult> DeleteAsync(Guid id);
}
