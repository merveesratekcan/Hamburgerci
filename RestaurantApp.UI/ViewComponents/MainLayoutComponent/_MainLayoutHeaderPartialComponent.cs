using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.UI.ViewComponents.MainLayoutComponent
{
    public class _MainLayoutHeaderPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
