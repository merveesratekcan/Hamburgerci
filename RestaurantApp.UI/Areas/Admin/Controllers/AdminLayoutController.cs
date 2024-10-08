using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    public class AdminLayoutController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
