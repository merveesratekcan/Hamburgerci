using RestaurantApp.Application.DTOs.UsersDTOs.UserAddressDTOs;

namespace RestaurantApp.UI.Areas.Admin.Models.AppUserVMs;

public class AdminAppUserCreateVM
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? ContactNumber { get; set; }
    public List<UserAddressCreateDTO>? Addresses { get; set; }
    //public IEnumerable<Order>? Orders { get; set; }
}
