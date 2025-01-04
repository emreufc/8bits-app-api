namespace _8bits_app_api.Dtos
{
    public class MissingIngredientDto
    {
        public int RecipeIngredientId { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string Unit { get; set; }
        public double? Quantity { get; set; }
        public int QuantityTypeId { get; set; }
        public bool IsDeleted { get; set; }
    }

}
