using _8bits_app_api.Models;

namespace _8bits_app_api.Repositories
{
    public interface IRecipeRepository
    {
        Task<(IEnumerable<Recipe> recipes, int totalCount)> GetAllRecipesAsync(int pageNumber, int pageSize);
        Task<Recipe> GetRecipeByIdAsync(int id);
    }
}
