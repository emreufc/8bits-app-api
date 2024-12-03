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

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }
    }
}
