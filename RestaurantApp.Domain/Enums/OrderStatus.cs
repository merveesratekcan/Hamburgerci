using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Domain.Enums;

public enum OrderStatus
{
    Approved = 1,
    Preparing = 2,
    Sending = 3,
    Finished = 4,
    Cancelled = 5
}   
