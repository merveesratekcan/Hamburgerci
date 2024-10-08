using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services.UserServices.GoogleCaptchaServices
{
    public interface IGoogleCaptchaService
    {
        Task<bool> VerifyToken(string token);
    }
}
