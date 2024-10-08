using RestaurantApp.Application.DTOs.WebDTOs.AboutDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.CampaignDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.OrderServices.AboutServices;

public interface IAboutService
{
    Task<IDataResult<List<AboutListDTO>>> GetAllAsync();
    Task<IDataResult<AboutDTO>> AddAsync(AboutCreateDTO aboutCreateDTO);
    Task<IDataResult<AboutDTO>> UpdateAsync(AboutUpdateDTO aboutUpdateDTO);
    Task<IDataResult<AboutDTO>> GetByIdAsync(Guid id);
    Task<IResult> DeleteAsync(Guid id);
}
