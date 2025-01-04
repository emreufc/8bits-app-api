using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class OldRecipeRepository : IOldRecipesRepository
    {
        private readonly mydbcontext _context;

        public OldRecipeRepository(mydbcontext context)
        {
            _context = context;
        }
        public async Task AddOldRecipeAsync(OldRecipe oldRecipe)
        {
            await _context.OldRecipes.AddAsync(oldRecipe);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Recipe>> GetOldRecipesByUserIdAsync(int userId)
        {
            return await _context.OldRecipes
                .Where(or => or.UserId == userId && !(or.IsDeleted ?? false))
                .Select(or => or.Recipe)
                .ToListAsync();
        }
        
        public async Task<bool> IsUserOldRecipeAsync(int userId, int recipeId)
        {
            return await _context.OldRecipes
                .AnyAsync(or => or.UserId == userId && or.RecipeId == recipeId && !(or.IsDeleted ?? false));
        }
        public async Task<bool> DeleteOldRecipeAsync(int recipeId, int userId)
        {
            var oldRecipe = await _context.OldRecipes
                .FirstOrDefaultAsync(or => or.RecipeId == recipeId && or.UserId == userId);

            if (oldRecipe != null)
            {
                _context.OldRecipes.Remove(oldRecipe);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
