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
            var exists = await _oldRecipesRepository.IsUserOldRecipeAsync(recipeId, userId);
            if (exists)
            {
                throw new Exception("Recipe is already in the old recipes.");
            }

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
        public async Task<bool> IsUserOldRecipeAsync(int userId, int recipeId)
        {
            return await _oldRecipesRepository.IsUserOldRecipeAsync(userId, recipeId);
        }
        public async Task<bool> DeleteRecipeFromOldRecipesAsync(int recipeId, int userId)
        {
            return await _oldRecipesRepository.DeleteOldRecipeAsync(recipeId, userId);
        }
    }
}
