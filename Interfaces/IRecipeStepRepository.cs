using _8bits_app_api.Models;

namespace _8bits_app_api.Repositories
{
    public interface IRecipeStepRepository
    {
        Task<IEnumerable<RecipeStep>> GetAllRecipeStepAsync();
        Task<RecipeStep> GetRecipeStepByIdAsync(int id);
        Task<IEnumerable<RecipeStep>> GetRecipeStepsByRecipeIdAsync(int recipeId);

    }
}