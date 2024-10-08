namespace RestaurantApp.UI.Models.Order
{
    public class ProductIngredientListVM
    {
        public Guid Id { get; set; }
        public bool IsOptional { get; set; }
        public decimal? IngredientPrice { get; set; }
        public Guid IngredientsId { get; set; }
        public string IngredientName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
