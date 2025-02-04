﻿using _8bits_app_api.Models;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Dtos;

namespace _8bits_app_api.Services
{
    public class FavoriteRecipeService : IFavoriteRecipeService
    {
        private readonly IFavoriteRecipeRepository _repository;

        public FavoriteRecipeService(IFavoriteRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<FavoriteRecipeDto> AddFavoriteAsync(FavoriteRecipeDto dto)
        {

            var favoriteRecipe = new FavoriteRecipe
            {
                UserId = dto.UserId,
                RecipeId = dto.RecipeId,
                IsDeleted = false
            };

            var result = await _repository.AddFavoriteAsync(favoriteRecipe);
            return new FavoriteRecipeDto
            {
                UserId = result.UserId ?? 0,
                RecipeId = result.RecipeId ?? 0,
                IsDeleted = result.IsDeleted ?? false
            };
        }

        public async Task<bool> RemoveFavoriteAsync(int userId, int recipeId)
        {
            return await _repository.RemoveFavoriteAsync(userId, recipeId);
        }

        public async Task<IEnumerable<Recipe>> GetFavoritesByUserIdAsync(int userId)
        {
            return await _repository.GetFavoritesByUserIdAsync(userId);
        }

        public async Task<bool> IsUserFavouriteAsync(int userId, int recipeId)
        {
            return await _repository.IsUserFavouriteAsync(userId, recipeId);
        }
      
    }
}
