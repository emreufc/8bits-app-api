using _8bits_app_api.Models;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class RecipeStepReadingService : IRecipeStepReadingService
    {
        private readonly IRecipeStepRepository _recipeStepRepository;
        public RecipeStepReadingService(IRecipeStepRepository recipeStepRepository)
        {
            _recipeStepRepository = recipeStepRepository;
        }
        public async Task<IEnumerable<RecipeStep>> GetAllRecipeStepAsync()
        {
            return await _recipeStepRepository.GetAllRecipeStepAsync();
        }

        public async Task<RecipeStep> GetRecipeStepByIdAsync(int id)
        {
            return await _recipeStepRepository.GetRecipeStepByIdAsync(id);
        }

        public async Task<IEnumerable<RecipeStep>> GetRecipeStepsByRecipeIdAsync(int recipeId)
        {
            return await _recipeStepRepository.GetRecipeStepsByRecipeIdAsync(recipeId);
        }

    }
}
