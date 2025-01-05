using _8bits_app_api.Models;
namespace _8bits_app_api.Dtos;

public class RecipeIngredientDto
{
    public int IngredientId { get; set; }

    public double Quantity { get; set; }

    public int QuantityTypeId { get; set; }
}