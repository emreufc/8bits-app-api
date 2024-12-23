﻿using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IRecipeIngredientReadingService
    {
        Task<(IEnumerable<RecipeIngredient> recipeIngredients, int totalCount)> GetRecipeIngredientsPaginatedAsync(int pageNumber, int pageSize);
        Task<RecipeIngredient> GetRecipeIngredientByIdAsync(int id);
        Task<(IEnumerable<RecipeIngredient> recipeIngredients, int totalCount)> GetRecipeIngredientsByRecipeIdPaginatedAsync(int recipeId, int pageNumber, int pageSize);
    }

}
