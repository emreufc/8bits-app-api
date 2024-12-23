using _8bits_app_api.Models;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class RecipeReadingService : IRecipeReadingService
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeReadingService(IRecipeRepository recipeRepository) 
        {
            _recipeRepository = recipeRepository;
        }
        public async Task<(IEnumerable<Recipe> recipes, int totalCount)> GetAllRecipesAsync(int pageNumber, int pageSize)
        {
            return await _recipeRepository.GetAllRecipesAsync(pageNumber, pageSize);
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _recipeRepository.GetRecipeByIdAsync(id);
        }
    }
}
