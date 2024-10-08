using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.ViewComponents.LayoutComponent
{
    public class _LayoutNavbarPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
