using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.UsersDTOs.MailDTOs
{
    public class SendMailDTO
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
    }
}
