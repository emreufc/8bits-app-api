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
        public async Task<IEnumerable<OldRecipe>> GetOldRecipesByUserIdAsync(int userId)
        {
            return await _context.OldRecipes
                                 .Where(or => or.UserId == userId)
                                 .OrderByDescending(or => or.AddedDate)
                                 .ToListAsync();
        }
    }
}
