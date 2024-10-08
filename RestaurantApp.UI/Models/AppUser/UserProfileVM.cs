using RestaurantApp.Domain.Entities.Users.AppUser;

namespace RestaurantApp.UI.Models.AppUser
{
    public class UserProfileVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? ContactNumber { get; set; }
        public List<UserAddress>? Addresses { get; set; }
    }
}
