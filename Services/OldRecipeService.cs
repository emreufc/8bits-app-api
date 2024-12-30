using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class OldRecipeService : IOldRecipesService
    {
        private readonly IOldRecipesRepository _oldRecipesRepository;

        public OldRecipeService(IOldRecipesRepository oldRecipesRepository)
        {
            _oldRecipesRepository = oldRecipesRepository;
        }
        public async Task AddRecipeToOldRecipesAsync(int recipeId, int userId)
        {
            var oldRecipe = new OldRecipe
            {
                RecipeId = recipeId,
                UserId = userId

            };
            await _oldRecipesRepository.AddOldRecipeAsync(oldRecipe);
        }
        public async Task<IEnumerable<OldRecipe>> GetOldRecipesByUserIdAsync(int userId)
        {
            return await _oldRecipesRepository.GetOldRecipesByUserIdAsync(userId);
        }
    }
}
