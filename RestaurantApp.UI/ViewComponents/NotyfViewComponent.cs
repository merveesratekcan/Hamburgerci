using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RestaurantApp.UI.Views.View_Component
{
    public class CustomNotyfViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
