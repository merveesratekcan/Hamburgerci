using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.UsersDTOs.AppUserDTOs;

public class AppUserChangePasswordDTO
{
    public Guid Id { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
