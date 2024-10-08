using RestaurantApp.Application.DTOs.WebDTOs.ContactDTOs;
using RestaurantApp.Application.DTOs.WebDTOs.FeatureDTOs;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.WebServices.FeatureServices;

public interface IFeatureService
{
    Task<IDataResult<List<FeatureListDTO>>> GetAllAsync();
    Task<IDataResult<FeatureDTO>> AddAsync(FeatureCreateDTO featureCreateDTO);
    Task<IDataResult<FeatureDTO>> UpdateAsync(FeatureUpdateDTO featureUpdateDTO);
    Task<IDataResult<FeatureDTO>> GetByIdAsync(Guid id);
    Task<IResult> DeleteAsync(Guid id);
}
