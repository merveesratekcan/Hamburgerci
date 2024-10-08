
using MailKit.Security;
using MimeKit;
using RestaurantApp.Application.DTOs.UsersDTOs.MailDTOs;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
namespace RestaurantApp.Application.Services.UserServices.EmailServices;

public class MailService : IMailService
{
  

    public async Task SendMailAsync(string email, string subject, string message)
    {
        try
        {
            var newMail = new MimeMessage();
            newMail.From.Add(MailboxAddress.Parse("hs14esra@gmail.com"));
            newMail.To.Add(MailboxAddress.Parse(email));
            newMail.Subject = subject;
            var builder = new BodyBuilder { HtmlBody = message };
            newMail.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("hs14esra@gmail.com", "pdpktbqnfnldegcl");
            await smtp.SendAsync(newMail);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"E-posta gönderilirken hata oluştu: {ex.Message}");
        }
    }
}