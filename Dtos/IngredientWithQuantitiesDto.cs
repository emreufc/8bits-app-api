using _8bits_app_api.Models;

namespace _8bits_app_api.Dtos
{
    public class IngredientWithQuantitiesDto
    {
        public Ingredient Ingredient { get; set; }

        public List<int> QuantityTypeIds { get; set; }
        public List<string> QuantityTypes { get; set; }
    }

}
