using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IRecipeStepReadingService
    {
        Task<IEnumerable<RecipeStep>> GetAllRecipeStepAsync();
        Task<RecipeStep> GetRecipeStepByIdAsync(int id);
        Task<IEnumerable<RecipeStep>> GetRecipeStepsByRecipeIdAsync(int recipeId);

    }
}
