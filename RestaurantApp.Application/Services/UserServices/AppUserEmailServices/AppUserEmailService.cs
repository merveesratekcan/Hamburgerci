using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MimeKit;
using RestaurantApp.Application.DTOs.UsersDTOs.MailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;

namespace RestaurantApp.Application.Services.UserServices.AppUserEmailServices;

public class AppUserEmailService : IAppUserEmailService
{
    public async Task SendAppMailAsync(SendMailDTO sendMailDTO)
    {
        try
        {
            var newMail = new MimeMessage();
            newMail.From.Add(MailboxAddress.Parse("melodyburger01@gmail.com"));
            newMail.To.Add(MailboxAddress.Parse(sendMailDTO.Email));
            newMail.Subject = sendMailDTO.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = sendMailDTO.Message;
            newMail.Body = builder.ToMessageBody();
            var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("melodyburger01@gmail.com","vcmemjmvluqxkygg");
            await smtp.SendAsync(newMail);
            await smtp.DisconnectAsync(true);

        }
        catch (Exception ex)
        {

            throw new InvalidOperationException($"E-posta Gönderiminde bir hata oluştu: {ex.Message}");
        }
    }
}
