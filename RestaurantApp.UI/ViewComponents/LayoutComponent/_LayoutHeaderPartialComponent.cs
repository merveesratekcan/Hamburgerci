using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.ViewComponents.LayoutComponent
{
    public class _LayoutHeaderPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
