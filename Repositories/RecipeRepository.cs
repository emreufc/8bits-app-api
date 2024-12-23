using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly mydbcontext _context;

        public RecipeRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Recipe> recipes, int totalCount)> GetAllRecipesAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Recipes.Where(p => p.IsDeleted == false).CountAsync();
            var recipes = await _context.Recipes
                .Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (recipes, totalCount);
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }
    }
}
