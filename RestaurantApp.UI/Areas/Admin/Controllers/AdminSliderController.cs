using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminSliderController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
