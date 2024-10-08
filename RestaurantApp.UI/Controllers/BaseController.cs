using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApp.UI.Models;

namespace RestaurantApp.UI.Controllers
{
    public class BaseController : Controller
    {

       
        protected INotyfService notyfService => HttpContext.RequestServices.GetService(typeof(INotyfService)) as INotyfService;

        protected void NotifySuccess(string messages)
        {
            notyfService.Success(messages);
        }
        protected void NotifyError(string messages)
        {
            notyfService.Error(messages);

        }
        protected void NotifyWarning(string message)
        {
            notyfService.Warning(message);
        }

        public void Notify(string message, string title = "",
                                   NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                message = message,
                title = title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = GetProvider()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }

        private string GetProvider()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProvider"];

            return value;
        }


    }




}
