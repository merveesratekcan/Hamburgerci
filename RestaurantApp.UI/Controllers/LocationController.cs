using Mapster;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Application.Services.OrderServices.AboutServices;
using RestaurantApp.Application.Services.WebServices.ContactServices;
using RestaurantApp.UI.Areas.Admin.Models.AboutVMs;
using RestaurantApp.UI.Areas.Admin.Models.ContactVMs;

namespace RestaurantApp.UI.Controllers
{
    public class LocationController : BaseController
    {
        private readonly IContactService _contactService;

        public LocationController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _contactService.GetAllAsync();
            if (!result.IsSuccess)
            {

                return View(result.Data.Adapt<List<AdminContactListVM>>());
            }

            return View(result.Data.Adapt<List<AdminContactListVM>>());
        }
    }
}
