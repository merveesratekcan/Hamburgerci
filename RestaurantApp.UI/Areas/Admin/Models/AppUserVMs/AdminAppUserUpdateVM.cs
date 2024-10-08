using RestaurantApp.Domain.Entities.Orders;

namespace RestaurantApp.UI.Areas.Admin.Models.AppUserVMs;

public class AdminAppUserUpdateVM
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? ContactNumber { get; set; }
    public List<string>? Addresses { get; set; }
    public IEnumerable<Order>? Orders { get; set; }
}
