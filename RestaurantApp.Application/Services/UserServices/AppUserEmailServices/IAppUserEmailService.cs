using RestaurantApp.Application.DTOs.UsersDTOs.MailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.UserServices.AppUserEmailServices;

public interface IAppUserEmailService
{
    Task SendAppMailAsync(SendMailDTO sendMailDTO);
}
