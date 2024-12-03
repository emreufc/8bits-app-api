using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IRecipeRateReadingService
    {
        Task<IEnumerable<RecipeRate>> GetAllRecipeRatesAsync();
        Task<RecipeRate> GetRecipeRateByIdAsync(int id);
    }
}
