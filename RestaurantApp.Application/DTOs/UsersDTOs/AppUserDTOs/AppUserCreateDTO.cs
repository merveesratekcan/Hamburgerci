using RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;
using RestaurantApp.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.UsersDTOs.AppUserDTOs;

public class AppUserCreateDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? ContactNumber { get; set; }
    public List<UserAddressCreateDTO>? Addresses { get; set; }
    public string IdentityId { get; set; }


    //public IEnumerable<Order>? Orders { get; set; }
}
