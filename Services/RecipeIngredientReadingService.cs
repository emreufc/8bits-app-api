using _8bits_app_api.Models;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class RecipeIngredientReadingService : IRecipeIngredientReadingService
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;

        public RecipeIngredientReadingService(IRecipeIngredientRepository recipeIngredientRepository)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
        }

        public async Task<(IEnumerable<RecipeIngredient> recipeIngredients, int totalCount)> GetRecipeIngredientsPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _recipeIngredientRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<RecipeIngredient>> GetRecipeIngredientByIdAsync(int id)
        {
            return await _recipeIngredientRepository.GetByIdAsync(id);
        }

        public async Task<(IEnumerable<RecipeIngredient> recipeIngredients, int totalCount)> GetRecipeIngredientsByRecipeIdPaginatedAsync(int recipeId, int pageNumber, int pageSize)
        {
            return await _recipeIngredientRepository.GetByRecipeIdPaginatedAsync(recipeId, pageNumber, pageSize);
        }
    }

}