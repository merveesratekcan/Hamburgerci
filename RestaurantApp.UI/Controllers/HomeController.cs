using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantApp.UI.Models;
using System.Diagnostics;

namespace RestaurantApp.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;      

        public HomeController(ILogger<HomeController> logger,IStringLocalizer<ModelResource> stringLocalize = null)
		{
			_logger = logger;
            _stringLocalizer = stringLocalize;
        }


		public IActionResult Index()
		{
            return RedirectToAction("Index", "DefaultMain");
        }

	

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
