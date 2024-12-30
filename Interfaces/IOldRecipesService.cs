using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IOldRecipesService
    {
        Task AddRecipeToOldRecipesAsync(int recipeId, int userId);
        Task<IEnumerable<OldRecipe>> GetOldRecipesByUserIdAsync(int userId);
    }
}
