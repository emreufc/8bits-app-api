﻿using _8bits_app_api.Dtos;
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
        public async Task<(IEnumerable<RecipeWithMatchDto> recipes, int totalCount)> GetAllRecipesWithMatchAsync(int userId, int pageNumber, int pageSize)
        {
            return await _recipeRepository.GetAllRecipesWithMatchAsync(userId, pageNumber, pageSize);
        }
        
        public async Task<(IEnumerable<Recipe> recipes, int totalCount)> GetRecipesByKeywordAsync(string keyword, int pageNumber, int pageSize)
        {
            return await _recipeRepository.GetRecipesByKeywordAsync(keyword, pageNumber, pageSize);
        }
        public async Task<(IEnumerable<Recipe> recipes, int totalCount)> GetFilteredRecipes(int userId, List<string> selectedCategories, int pageNumber, int pageSize)
        {
            return await _recipeRepository.GetFilteredRecipes(userId, selectedCategories, pageNumber, pageSize);
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            await _recipeRepository.AddRecipeAsync(recipe);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            await _recipeRepository.UpdateRecipeAsync(recipe);
        }

        public async Task DeleteRecipeAsync(int recipeId)
        {
            await _recipeRepository.DeleteRecipeAsync(recipeId);
        }
    }
}
