namespace _8bits_app_api.Dtos
{
    public class InventoryDto
    {
        public int InventoryId { get; set; }
        public int UserId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientCategory { get; set; }
        public string IngredientImageUrl { get; set; }
        public double Quantity { get; set; }
        public string QuantityTypeDesc { get; set; }
        public bool IsDeleted { get; set; }
    }

}
