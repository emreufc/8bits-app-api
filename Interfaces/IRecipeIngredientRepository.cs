using _8bits_app_api.Models;

namespace _8bits_app_api.Repositories
{
    public interface IRecipeIngredientRepository
    {
        Task<(IEnumerable<RecipeIngredient> recipeIngredients, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<RecipeIngredient> GetByIdAsync(int id);
        Task<(IEnumerable<RecipeIngredient> recipeIngredients, int totalCount)> GetByRecipeIdPaginatedAsync(int recipeId, int pageNumber, int pageSize);
    }

}