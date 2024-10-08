using RestaurantApp.Application.DTOs.UsersDTOs.MailDTOs;

namespace RestaurantApp.Application.Services.UserServices.EmailServices;

public interface IMailService
{
    Task SendMailAsync(string email, string subject, string message);
   

}
