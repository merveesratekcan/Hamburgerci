using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.Controllers
{
    public class MainLayoutController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
