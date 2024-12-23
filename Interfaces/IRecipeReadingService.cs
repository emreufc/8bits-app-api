using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IRecipeReadingService
    {
        Task<(IEnumerable<Recipe> recipes, int totalCount)> GetAllRecipesAsync(int pageNumber, int pageSize);
        Task<Recipe> GetRecipeByIdAsync(int id);
    }
}
