using System.ComponentModel.DataAnnotations;
namespace RestaurantApp.UI.Models;

public class LoginVM
{
    //public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public string Token { get; set; }

}

