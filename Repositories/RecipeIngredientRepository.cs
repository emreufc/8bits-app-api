using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        private readonly mydbcontext _context;

        public RecipeIngredientRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<RecipeIngredient> recipeIngredients, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.RecipeIngredients.Where(p => p.IsDeleted == false).CountAsync();
            var recipeIngredients = await _context.RecipeIngredients.Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (recipeIngredients, totalCount);
        }

        public async Task<RecipeIngredient> GetByIdAsync(int id)
        {
            return await _context.RecipeIngredients.FindAsync(id);
        }

        public async Task<(IEnumerable<RecipeIngredient> recipeIngredients, int totalCount)> GetByRecipeIdPaginatedAsync(int recipeId, int pageNumber, int pageSize)
        {
            var totalCount = await _context.RecipeIngredients.CountAsync(ri => ri.RecipeId == recipeId && ri.IsDeleted == false);
            var recipeIngredients = await _context.RecipeIngredients
                .Where(ri => ri.RecipeId == recipeId && ri.IsDeleted == false)
                .Include(ri => ri.Ingredient)
                .Include(ri => ri.QuantityType)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (recipeIngredients, totalCount);
        }
    }

}