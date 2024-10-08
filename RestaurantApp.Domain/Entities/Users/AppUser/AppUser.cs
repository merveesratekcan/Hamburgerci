using RestaurantApp.Domain.Core.BaseEntities;
using RestaurantApp.Domain.Entities.Orders;

namespace RestaurantApp.Domain.Entities.Users.AppUser;

public class AppUser : BaseUser
{
    public string? ContactNumber { get; set; }
    //Fotoğraf ve Tel No Opsiyonel!!!
    //Nav Prop
    public virtual IEnumerable<UserAddress>? Addresses { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
    public AppUser()
    {
        Orders = new HashSet<Order>();
    }
}
