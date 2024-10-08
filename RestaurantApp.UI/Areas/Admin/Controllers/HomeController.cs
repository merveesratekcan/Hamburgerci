using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {

        public IActionResult Index()
        {
            return View();

        }
    }
}
