namespace _8bits_app_api.Dtos
{
    public class ShoppingListResponseDto
    {
        public int ShoppingListId { get; set; }
        public int? UserId { get; set; }
        public int? IngredientId { get; set; }
        public int? QuantityTypeId { get; set; }
        public string? Quantity { get; set; }
        public string? IngredientName { get; set; }
        public string? IngImgUrl { get; set; }
        public string? QuantityTypeDesc { get; set; }
    }
}
