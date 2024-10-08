using Mapster;
using RestaurantApp.Application.DTOs.WebDTOs.SliderDTOs;
using RestaurantApp.Domain.Contracts.WebsitesContracts.SliderRepositories;
using RestaurantApp.Domain.Entities.Websites;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.WebServices.SliderServices;

public class SliderService : ISliderService
{
    private readonly ISliderRepository _sliderRepository;

    public SliderService(ISliderRepository sliderRepository)
    {
        _sliderRepository = sliderRepository;
    }

    public async Task<IDataResult<SliderDTO>> AddAsync(SliderCreateDTO sliderCreateDTO)
    {
       
        var newSlider = sliderCreateDTO.Adapt<Slider>();
        await _sliderRepository.AddAsync(newSlider);
        await _sliderRepository.SaveChangesAsync();
        return new SuccessDataResult<SliderDTO>(newSlider.Adapt<SliderDTO>(), "Slider Add success!");
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
       var slider = await _sliderRepository.GetByIdAsync(id);
        if (slider is null)
        {
            return new ErrorResult("Slider not found");
        }
        await _sliderRepository.DeleteAsync(slider);
        await _sliderRepository.SaveChangesAsync();
        return new SuccessResult("Slider delete success!");
    }

    public async Task<IDataResult<List<SliderListDTO>>> GetAllAsync()
    {
       var slider = await _sliderRepository.GetAllAsync();
        if (slider.Count() <= 0)
        {
            return new ErrorDataResult<List<SliderListDTO>>(slider.Adapt<List<SliderListDTO>>(), "Slider Sistemde Kayıtlı");
        }
        return new SuccessDataResult<List<SliderListDTO>>(slider.Adapt<List<SliderListDTO>>(), "Slider List success!");
    }

    public async Task<IDataResult<SliderDTO>> GetByIdAsync(Guid id)
    {
        var slider = await _sliderRepository.GetByIdAsync(id);
        if (slider is null)
        {
            return new ErrorDataResult<SliderDTO>("Slider not found");
        }
        return new SuccessDataResult<SliderDTO>(slider.Adapt<SliderDTO>(), "Slider Get success!");
        }

    public async Task<IDataResult<SliderDTO>> UpdateAsync(SliderUpdateDTO sliderUpdateDTO)
    {
       var slider = await _sliderRepository.GetByIdAsync(sliderUpdateDTO.Id);
        if (slider is null)
        {
            return new ErrorDataResult<SliderDTO>("Slider not found");
        }
    
        var updatedSlider = sliderUpdateDTO.Adapt(slider);

        updatedSlider.Title1 = sliderUpdateDTO.Title1;
        updatedSlider.Title2 = sliderUpdateDTO.Title2;
        updatedSlider.Title3 = sliderUpdateDTO.Title3;
        updatedSlider.Description1 = sliderUpdateDTO.Description1;
        updatedSlider.Description2 = sliderUpdateDTO.Description2;
        updatedSlider.Description3 = sliderUpdateDTO.Description3;

        

        await _sliderRepository.UpdateAsync(updatedSlider);
        await _sliderRepository.SaveChangesAsync();
        return new SuccessDataResult<SliderDTO>(updatedSlider.Adapt<SliderDTO>(), "Slider Update success!");
    }
}
