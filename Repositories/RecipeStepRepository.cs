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

        public async Task AddRecipeStepsAsync(int recipeId, List<RecipeStep> steps)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId && !(r.IsDeleted ?? false));

            if (recipe == null)
            {
                throw new KeyNotFoundException("Recipe not found or deleted.");
            }

            // Get the current maximum RecipeStepsId
            var maxId = await _context.RecipeSteps
                .Where(rs => !(rs.IsDeleted ?? false))
                .MaxAsync(rs => (int?)rs.RecipeStepsId) ?? 0;

            byte stepNum = 1;

            foreach (var step in steps)
            {
                maxId++; // Increment ID for each new step

                step.RecipeStepsId = maxId;
                step.RecipeId = recipeId;
                step.RecipeName=recipe.RecipeName; 
                step.StepNum = stepNum;
                step.IsDeleted = false; // Ensure it's not marked as deleted
                stepNum++;

                await _context.RecipeSteps.AddAsync(step);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecipeStepAsync(int recipeId,RecipeStep step)
        {
            var existingStep = await _context.RecipeSteps
                .FirstOrDefaultAsync(rs => rs.RecipeId == recipeId 
                                           && rs.RecipeStepsId == step.RecipeStepsId 
                                           && !(rs.IsDeleted ?? false));

            if (existingStep == null)
            {
                throw new KeyNotFoundException("RecipeStep not found or deleted.");
            }

            // Update fields
            existingStep.StepNum = step.StepNum;
            existingStep.Step = step.Step;

            _context.RecipeSteps.Update(existingStep);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecipeStepAsync(int recipeId, byte stepNum)
        {
            // Fetch the RecipeStep with the given RecipeId and StepNum
            var step = await _context.RecipeSteps
                .FirstOrDefaultAsync(rs => rs.RecipeId == recipeId 
                                           && rs.StepNum == stepNum 
                                           && !(rs.IsDeleted ?? false));

            if (step == null)
            {
                throw new KeyNotFoundException("RecipeStep not found or already deleted.");
            }

            // Perform soft delete
            step.IsDeleted = true;

            _context.RecipeSteps.Update(step);
            await _context.SaveChangesAsync();
        }
    }

}
