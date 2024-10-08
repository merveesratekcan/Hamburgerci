using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.Controllers
{
    public class MessageController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
