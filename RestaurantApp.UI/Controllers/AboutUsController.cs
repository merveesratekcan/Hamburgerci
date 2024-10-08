using Mapster;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.Services.OrderServices.AboutServices;
using RestaurantApp.UI.Areas.Admin.Models.AboutVMs;

namespace RestaurantApp.UI.Controllers;

public class AboutUsController : BaseController
{
    private readonly IAboutService _aboutService;

    public AboutUsController (IAboutService aboutService)
    {
        _aboutService = aboutService;
    }
    public async Task<IActionResult> Index()
    {
        var result = await _aboutService.GetAllAsync();
        if (!result.IsSuccess)
        {
            
            return View(result.Data.Adapt<List<AdminAboutListVM>>());
        }
       
        return View(result.Data.Adapt<List<AdminAboutListVM>>());
    }
}
