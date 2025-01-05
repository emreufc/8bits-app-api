using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IRecipeStepReadingService
    {
        Task<(IEnumerable<RecipeStep> recipeSteps, int totalCount)> GetRecipeStepsPaginatedAsync(int pageNumber, int pageSize);
        Task<RecipeStep> GetRecipeStepByIdAsync(int id);
        Task<(IEnumerable<RecipeStep> recipeSteps, int totalCount)> GetRecipeStepsByRecipeIdPaginatedAsync(int recipeId, int pageNumber, int pageSize);
        Task AddRecipeStepsAsync(int recipeId, List<RecipeStep> steps);
        Task UpdateRecipeStepAsync(int recipeId, RecipeStep step);
        Task DeleteRecipeStepAsync(int recipeId, byte stepNum);
    }

}
