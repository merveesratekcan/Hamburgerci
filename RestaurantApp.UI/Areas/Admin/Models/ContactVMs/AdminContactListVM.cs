namespace RestaurantApp.UI.Areas.Admin.Models.ContactVMs;

public class AdminContactListVM
{
    public Guid Id { get; set; }
    
    public string ContactLocation { get; set; }
    public string ContactNumber { get; set; }
    public string ContactMail { get; set; }
    public string ContactFooterDescription { get; set; }
}
