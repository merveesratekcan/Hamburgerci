using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.ViewComponents.MainLayoutComponent
{
    public class _MainLayoutNavbarPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
