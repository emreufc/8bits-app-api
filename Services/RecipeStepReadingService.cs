﻿using _8bits_app_api.Models;
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

        public async Task<(IEnumerable<RecipeStep> recipeSteps, int totalCount)> GetRecipeStepsPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _recipeStepRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<RecipeStep> GetRecipeStepByIdAsync(int id)
        {
            return await _recipeStepRepository.GetByIdAsync(id);
        }

        public async Task<(IEnumerable<RecipeStep> recipeSteps, int totalCount)> GetRecipeStepsByRecipeIdPaginatedAsync(int recipeId, int pageNumber, int pageSize)
        {
            return await _recipeStepRepository.GetByRecipeIdPaginatedAsync(recipeId, pageNumber, pageSize);
        }

        public async Task AddRecipeStepsAsync(int recipeId, List<RecipeStep> steps)
        {
            await _recipeStepRepository.AddRecipeStepsAsync(recipeId, steps);
        }

        public async Task UpdateRecipeStepAsync(int recipeId,RecipeStep step)
        {
            await _recipeStepRepository.UpdateRecipeStepAsync(recipeId,step);
        }

        public async Task DeleteRecipeStepAsync(int recipeId, byte stepNum)
        {
            await _recipeStepRepository.DeleteRecipeStepAsync(recipeId, stepNum);
        }
    }

}
