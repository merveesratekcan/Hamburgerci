﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;

public class UserAddressDTO
{
    public Guid Id { get; set; }
    public string? Address { get; set; }
}
