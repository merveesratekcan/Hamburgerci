using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.Controllers
{
    public class DefaultMainController : Controller
    {
        public IActionResult Index()
        
        {
            return View();
        }
    }
}
