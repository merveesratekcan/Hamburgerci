using Mapster;
using RestaurantApp.Application.DTOs.WebDTOs.FeatureDTOs;
using RestaurantApp.Domain.Contracts.WebsitesContracts.FeatureRepositories;
using RestaurantApp.Domain.Entities.Websites;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.WebServices.FeatureServices;

public class FeatureService : IFeatureService
{
    private readonly IFeatureRepository _featureRepository;

    public FeatureService(IFeatureRepository featureRepository)
    {
        _featureRepository = featureRepository;
    }

    public async Task<IDataResult<FeatureDTO>> AddAsync(FeatureCreateDTO featureCreateDTO)
    {
        if (await _featureRepository.AnyAsync(x => x.FeatureTitle == featureCreateDTO.FeatureTitle))
        {
            return new ErrorDataResult<FeatureDTO>("Feature already exists");
        }
        var newFeature = featureCreateDTO.Adapt<Feature>();
        await _featureRepository.AddAsync(newFeature);
        await _featureRepository.SaveChangesAsync();
        return new SuccessDataResult<FeatureDTO>(newFeature.Adapt<FeatureDTO>(), "Feature Add success!");

    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var feature = await _featureRepository.GetByIdAsync(id);
        if (feature is null)
        {
            return new ErrorResult("Feature not found");
        }
        await _featureRepository.DeleteAsync(feature);
        await _featureRepository.SaveChangesAsync();
        return new SuccessResult("Feature delete success!");
    }

    public async Task<IDataResult<List<FeatureListDTO>>> GetAllAsync()
    {
        var feature = await _featureRepository.GetAllAsync();
        if (feature.Count() <= 0)
        {
            return new ErrorDataResult<List<FeatureListDTO>>(feature.Adapt<List<FeatureListDTO>>(), "Feature Sistemde Kayıtlı");
        }
        return new SuccessDataResult<List<FeatureListDTO>>(feature.Adapt<List<FeatureListDTO>>(), "Feature List success!");
    }

    public async Task<IDataResult<FeatureDTO>> GetByIdAsync(Guid id)
    {
       var feature = await _featureRepository.GetByIdAsync(id);
        if (feature is null)
        {
            return new ErrorDataResult<FeatureDTO>("Feature not found");
        }
        return new SuccessDataResult<FeatureDTO>(feature.Adapt<FeatureDTO>(), "Feature Get success!");
    }

    public async Task<IDataResult<FeatureDTO>> UpdateAsync(FeatureUpdateDTO featureUpdateDTO)
    {
       var feature = await _featureRepository.GetByIdAsync(featureUpdateDTO.Id);
        if (feature is null)
        {
            return new ErrorDataResult<FeatureDTO>("Feature not found");
        }
        if (await _featureRepository.AnyAsync(x => x.FeatureTitle == featureUpdateDTO.FeatureTitle))
        {
            return new ErrorDataResult<FeatureDTO>("Feature already exists");
        }
        featureUpdateDTO.Adapt(feature);
        await _featureRepository.UpdateAsync(feature);
        await _featureRepository.SaveChangesAsync();
        return new SuccessDataResult<FeatureDTO>(feature.Adapt<FeatureDTO>(), "Feature Update success!");
    }
}
