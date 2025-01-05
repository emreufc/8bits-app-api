using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Repositories
{
    public interface IRecipeRepository
    {
        Task<(IEnumerable<Recipe> recipes, int totalCount)> GetAllRecipesAsync(int pageNumber, int pageSize);
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<(IEnumerable<RecipeWithMatchDto> recipes, int totalCount)> GetAllRecipesWithMatchAsync(int userId, int pageNumber, int pageSize);
        Task<(IEnumerable<Recipe> recipes, int totalCount)> GetRecipesByKeywordAsync(string keyword,int pageNumber,int pageSize);
        Task<(IEnumerable<Recipe> recipes, int totalCount)> GetFilteredRecipes(int userId, List<string> selectedCategories,int pageNumber, int pageSize);
        Task AddRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(int recipeId);
    }
}
