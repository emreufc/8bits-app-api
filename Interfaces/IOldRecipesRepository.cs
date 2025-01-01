using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IOldRecipesRepository
    {
        Task AddOldRecipeAsync(OldRecipe oldRecipe);
        Task<IEnumerable<OldRecipe>> GetOldRecipesByUserIdAsync(int userId);
        Task<bool> IsUserOldRecipeAsync(int userId, int recipeId);
        Task<bool> DeleteOldRecipeAsync(int recipeId, int userId);
        
    }
}
