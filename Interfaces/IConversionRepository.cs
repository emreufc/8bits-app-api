using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IConversionRepository
    {
        Task<IEnumerable<IngredientConversion>> GetConversionsByIngredientIdAsync(int ingredientId);
        Task<IngredientConversion?> GetConversionbyQuantitytypeIdAsync(int ingredientId, int quantityTypeId);
    }
}
