using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using RestaurantApp.UI.Areas.Admin.Models.MailVMs;
using System.Net.Mail;
using Microsoft.Extensions.Localization;

namespace RestaurantApp.UI.Areas.Admin.Controllers
{
    
    public class MailController : AdminBaseController
    {
        private readonly IStringLocalizer<ModelResource> _stringLocalizer;

        public MailController(IStringLocalizer<ModelResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailCreateVM mailCreateVM)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress fromAddress = new MailboxAddress("MelodyBurger", "melodyburger01@gmail.com");
            message.From.Add(fromAddress);

            if (string.IsNullOrEmpty(mailCreateVM.Email))
            {
              
                ModelState.AddModelError("Email", "Recipient email address is required.");
                Notify(_stringLocalizer["Recipient email address is required."], notificationType: UI.Models.NotificationType.error);
                return View(mailCreateVM);
            }

            MailboxAddress toAddress = new MailboxAddress("AppUser", mailCreateVM.Email);
            message.To.Add(toAddress);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailCreateVM.Message;
            message.Body = bodyBuilder.ToMessageBody();
            message.Subject = mailCreateVM.Subject;

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("melodyburger01@gmail.com", "vcmemjmvluqxkygg"); // Use an app-specific password
                    client.Send(message);
                    client.Disconnect(true);
                }

                Notify(_stringLocalizer["Email sent successfully!"], notificationType: UI.Models.NotificationType.success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Notify(_stringLocalizer["Failed to send email: "] + ex.Message, notificationType: UI.Models.NotificationType.error);
                return View(mailCreateVM);
            }
           
           
        }
    }
}
