using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Entities.GoogleCaptchas
{
    public class GoogleCaptchaResponse
    {
        public bool Success { get; set; }
        public double Score { get; set; }
    }
}
