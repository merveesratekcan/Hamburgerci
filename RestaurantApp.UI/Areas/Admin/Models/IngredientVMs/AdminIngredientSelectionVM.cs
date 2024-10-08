namespace RestaurantApp.UI.Areas.Admin.Models.IngredientVMs
{
    public class AdminIngredientSelectionVM
    {
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public bool IsOptional { get; set; }
        public bool Selected { get; set; }
    }
}
