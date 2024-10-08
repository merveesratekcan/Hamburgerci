using RestaurantApp.Domain.Entities.Orders;
using RestaurantApp.Domain.Entities.Users.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.DTOs.UsersDTOs.AppUserDTOs;

public class AppUserDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? ContactNumber { get; set; }
    public ICollection<string>? Addresses { get; set; }
    public string? IdentityId { get; set; }


    //public IEnumerable<Order>? Orders { get; set; }


}
