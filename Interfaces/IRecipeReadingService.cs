using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IRecipeReadingService
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
    }
}
