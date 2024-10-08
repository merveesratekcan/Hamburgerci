using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.ViewComponents.LayoutComponent
{
    public class _LayoutSidebarPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
