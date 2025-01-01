using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace _8bits_app_api.Repositories
{
    public class FavoriteRecipeRepository : IFavoriteRecipeRepository
    {
        private readonly mydbcontext _context;

        public FavoriteRecipeRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<FavoriteRecipe> AddFavoriteAsync(FavoriteRecipe favoriteRecipe)
        {
            _context.FavoriteRecipes.Add(favoriteRecipe);
            await _context.SaveChangesAsync();
            return favoriteRecipe;
        }

        public async Task<bool> RemoveFavoriteAsync(int userId, int recipeId)
        {
            var favorite = await _context.FavoriteRecipes
                .FirstOrDefaultAsync(f => f.UserId == userId && f.RecipeId == recipeId && !(f.IsDeleted ?? false));

            if (favorite != null)
            {
                _context.FavoriteRecipes.Remove(favorite);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Recipe>> GetFavoritesByUserIdAsync(int userId)
        {
            return await _context.FavoriteRecipes
                .Where(f => f.UserId == userId && !(f.IsDeleted ?? false))
                .Select(f => f.Recipe)
                .ToListAsync();
        }
        
        public async Task<bool> IsUserFavouriteAsync(int userId, int recipeId)
        {
            return await _context.FavoriteRecipes
                .AnyAsync(fr => fr.UserId == userId && fr.RecipeId == recipeId && !(fr.IsDeleted ?? false));
        }
       
    }
}
