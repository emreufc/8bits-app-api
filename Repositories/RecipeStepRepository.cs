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

        public async Task<(IEnumerable<RecipeStep> recipeSteps, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.RecipeSteps.Where(p => p.IsDeleted == false).CountAsync();
            var recipeSteps = await _context.RecipeSteps
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (recipeSteps, totalCount);
        }

        public async Task<RecipeStep> GetByIdAsync(int id)
        {
            return await _context.RecipeSteps.FindAsync(id);
        }

        public async Task<(IEnumerable<RecipeStep> recipeSteps, int totalCount)> GetByRecipeIdPaginatedAsync(int recipeId, int pageNumber, int pageSize)
        {
            var totalCount = await _context.RecipeSteps.CountAsync(rs => rs.RecipeId == recipeId && rs.IsDeleted == false);
            var recipeSteps = await _context.RecipeSteps
                .Where(rs => rs.RecipeId == recipeId && rs.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (recipeSteps, totalCount);
        }
    }

}
