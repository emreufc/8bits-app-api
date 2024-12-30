using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IOldRecipesRepository
    {
        Task AddOldRecipeAsync(OldRecipe oldRecipe);
        Task<IEnumerable<OldRecipe>> GetOldRecipesByUserIdAsync(int userId);
    }
}
