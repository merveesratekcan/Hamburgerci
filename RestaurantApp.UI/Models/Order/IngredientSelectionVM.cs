namespace RestaurantApp.UI.Models.Order
{
    public class IngredientSelectionVM
    {
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public bool IsOptional { get; set; }
        public decimal OptionalPrice { get; set; }
        public bool Selected { get; set; }
    }
}
