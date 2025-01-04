using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IOldRecipesService
    {
        Task AddRecipeToOldRecipesAsync(int recipeId, int userId);
        Task<IEnumerable<Recipe>> GetOldRecipesByUserIdAsync(int userId);
        Task<bool> IsUserOldRecipeAsync(int userId, int recipeId);
        Task<bool> DeleteRecipeFromOldRecipesAsync(int recipeId, int userId);
    }
}
