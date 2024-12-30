using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IConversionService
    {
        Task<IEnumerable<IngredientConversion>> GetConversionsByIngredientIdAsync(int ingredientId);
        Task<ConversionResult> ConvertToStandardUnitAsync(int ingredientId, int quantityTypeId, double quantity);
    }
}
