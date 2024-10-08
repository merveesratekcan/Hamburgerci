using System.ComponentModel.DataAnnotations;
namespace RestaurantApp.UI.Areas.Admin.Models.AdminVMs;

public class AdminAdminChangePasswordVM
{


    public Guid Id { get; set; }
    [Required, DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Required, DataType(DataType.Password)]
    public string NewPassword { get; set; }
    [Required, DataType(DataType.Password)]
    [Compare(nameof(NewPassword))]
    public string ConfirmNewPassword { get; set; }
    //public string Token { get; set; }

}
