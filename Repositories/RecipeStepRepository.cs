using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class RecipeStepRepository : IRecipeStepRepository
    {
        private readonly mydbcontext _context;

        public RecipeStepRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecipeStep>> GetAllRecipeStepAsync()
        {
            return await _context.RecipeSteps.ToListAsync();
        }

        public async Task<RecipeStep> GetRecipeStepByIdAsync(int id)
        {
            return await _context.RecipeSteps.FindAsync(id);
        }

        public async Task<IEnumerable<RecipeStep>> GetRecipeStepsByRecipeIdAsync(int recipeId)
        {
            return await _context.RecipeSteps
                                 .Where(rs => rs.RecipeId == recipeId)
                                 .ToListAsync();
        }

    }
}
