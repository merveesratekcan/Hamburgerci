using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
