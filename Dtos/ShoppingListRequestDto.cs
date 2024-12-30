using System.Text.Json.Serialization;

namespace _8bits_app_api.Dtos
{
    public class ShoppingListRequestDto
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public int IngredientId { get; set; }

        public int QuantityTypeId { get; set; }

        public double Quantity { get; set; }

        [JsonIgnore]
        public bool? IsDeleted { get; set; }
    }
}
