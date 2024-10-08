using RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.UI.Models.AppUser
{
    public class UserRegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? ContactNumber { get; set; }
        public List<UserAddressCreateDTO>? Addresses { get; set; }
    }
}
