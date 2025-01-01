using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IRecipeReadingService
    {
        Task<(IEnumerable<Recipe> recipes, int totalCount)> GetAllRecipesAsync(int pageNumber, int pageSize);
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<(IEnumerable<RecipeWithMatchDto> recipes, int totalCount)> GetAllRecipesWithMatchAsync(int userId, int pageNumber, int pageSize);
        Task<(IEnumerable<Recipe> recipes, int totalCount)> GetFilteredRecipes(int userId, List<string> selectedCategories, int pageNumber, int pageSize);
    }
}
