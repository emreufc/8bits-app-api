﻿using _8bits_app_api.Models;

namespace _8bits_app_api.Repositories
{
    public interface IRecipeStepRepository
    {
        Task<(IEnumerable<RecipeStep> recipeSteps, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<RecipeStep> GetByIdAsync(int id);
        Task<(IEnumerable<RecipeStep> recipeSteps, int totalCount)> GetByRecipeIdPaginatedAsync(int recipeId, int pageNumber, int pageSize);
        Task AddRecipeStepsAsync(int recipeId, List<RecipeStep> steps);
        Task UpdateRecipeStepAsync(int recipeId, RecipeStep step);
        Task DeleteRecipeStepAsync(int recipeId, byte stepNum);
    }

}