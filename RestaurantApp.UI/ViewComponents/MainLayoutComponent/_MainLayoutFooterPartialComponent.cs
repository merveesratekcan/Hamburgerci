using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.ViewComponents.MainLayoutComponent
{
    public class _MainLayoutFooterPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
