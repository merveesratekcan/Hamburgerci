using Mapster;
using RestaurantApp.Application.DTOs.WebDTOs.AboutDTOs;
using RestaurantApp.Domain.Contracts.WebsitesContracts.AboutUsRepositories;
using RestaurantApp.Domain.Entities.Websites;
using RestaurantApp.Domain.Utilities.Concretes;
using RestaurantApp.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.OrderServices.AboutServices;

public class AboutService : IAboutService
{
    private readonly IAboutUsRepository _aboutRepository;

    public AboutService(IAboutUsRepository aboutRepository)
    {
        _aboutRepository = aboutRepository;
    }

    public async Task<IDataResult<AboutDTO>> AddAsync(AboutCreateDTO aboutCreateDTO)
    {
       if (await _aboutRepository.AnyAsync(x => x.Title.ToLower() == aboutCreateDTO.Title.ToLower()))
        {
            return new ErrorDataResult<AboutDTO>("About already exists");
        }
        var newAbout = aboutCreateDTO.Adapt<AboutUs>();
        await _aboutRepository.AddAsync(newAbout);
        await _aboutRepository.SaveChangesAsync();
        return new SuccessDataResult<AboutDTO>(newAbout.Adapt<AboutDTO>(), "About Add success!");

    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var about = await _aboutRepository.GetByIdAsync(id);
        if (about is null)
        {
            return new ErrorResult("About not found");
        }
        await _aboutRepository.DeleteAsync(about);
        await _aboutRepository.SaveChangesAsync();
        return new SuccessResult("About delete success!");
    }

    public async Task<IDataResult<List<AboutListDTO>>> GetAllAsync()
    {
        var about = await _aboutRepository.GetAllAsync();
        if (about.Count() <= 0)
        {
            return new ErrorDataResult<List<AboutListDTO>>(about.Adapt<List<AboutListDTO>>(), "About Sistemde Kayıtlı");
        }
        return new SuccessDataResult<List<AboutListDTO>>(about.Adapt<List<AboutListDTO>>(), "About List success!");
    }

    public async Task<IDataResult<AboutDTO>> GetByIdAsync(Guid id)
    {
        var about = await _aboutRepository.GetByIdAsync(id);
        if (about == null)
        {
            return new ErrorDataResult<AboutDTO>(about.Adapt < AboutDTO >() ,"About not found");
        }
        return new SuccessDataResult<AboutDTO>(about.Adapt<AboutDTO>(), "About get success!");
    }

    public async Task<IDataResult<AboutDTO>> UpdateAsync(AboutUpdateDTO aboutUpdateDTO)
    {
       var about = await _aboutRepository.GetByIdAsync(aboutUpdateDTO.Id);
        if (about is null)
        {
            return new ErrorDataResult<AboutDTO>("About not found");
        }
        if (await _aboutRepository.AnyAsync(x => x.Title.ToLower() == aboutUpdateDTO.Title.ToLower()))
        {
            return new ErrorDataResult<AboutDTO>("About already exists");
        }
       
        about.Title = aboutUpdateDTO.Title;
        about.Description = aboutUpdateDTO.Description;  

        await _aboutRepository.UpdateAsync(about);
        await _aboutRepository.SaveChangesAsync();
        return new SuccessDataResult<AboutDTO>(about.Adapt<AboutDTO>(), "About update success!");
    }
}
