using RestaurantApp.Application.DTOs.ProductsDTOs.CampaignDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.IngredientDTOs;
using RestaurantApp.Application.DTOs.ProductsDTOs.ProductDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.ProductsServices.CampaignServices;

public interface ICampaignService
{
    Task<IDataResult<List<CampaignListDTO>>> GetAllAsync();
    Task<IDataResult<CampaignDTO>> AddAsync(CampaignCreateDTO campaignCreateDTO);
    Task<IDataResult<CampaignDTO>> UpdateAsync(CampaignUpdateDTO campaignUpdateDTO);
    Task<IDataResult<CampaignDTO>> GetByIdAsync(Guid id);
    Task<IResult> DeleteAsync(Guid id);

    Task<IDataResult<List<ProductDTO>>> GetProductByCampaignIdAsync(Guid campaignId);

    Task ActivateCampaign(Guid campaignId);
    Task DeactivateCampaign(Guid campaignId);
}
