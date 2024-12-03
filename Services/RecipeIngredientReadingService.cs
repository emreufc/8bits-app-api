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
        public async Task<IEnumerable<RecipeIngredient>> GetAllRecipeIngredientAsync()
        {
            return await _recipeIngredientRepository.GetAllRecipeIngredientAsync();
        }

        public async Task<RecipeIngredient> GetRecipeIngredientByIdAsync(int id)
        {
            return await _recipeIngredientRepository.GetRecipeIngredientByIdAsync(id);
        }

        public async Task<IEnumerable<RecipeIngredient>> GetRecipeIngredientsByRecipeIdAsync(int recipeId)
        {
            return await _recipeIngredientRepository.GetRecipeIngredientsByRecipeIdAsync(recipeId);
        }

    }
}