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

        public async Task<IEnumerable<RecipeIngredient>> GetByIdAsync(int id)
        {
            return await _context.RecipeIngredients
                 .Include(ri => ri.Ingredient)
                .Include(ri => ri.QuantityType)
                .Where(ri => ri.RecipeId == id && ri.IsDeleted == false)
                .ToListAsync(); 
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

        public async Task AddIngredientsAsync(int recipeId, List<RecipeIngredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {

                // Assign the necessary values
                ingredient.RecipeId = recipeId;
                ingredient.IsDeleted = false; // Ensure it is not marked as deleted

                // Add the ingredient to the context
                await _context.RecipeIngredients.AddAsync(ingredient);
            }

            await _context.SaveChangesAsync(); // Save changes to the database
        }

        public async Task UpdateIngredientAsync(RecipeIngredient recipeIngredient)
        {
            var existingIngredient = await _context.RecipeIngredients
                .FirstOrDefaultAsync(ri => ri.RecipeId == recipeIngredient.RecipeId 
                                           && ri.IngredientId == recipeIngredient.IngredientId 
                                           && !(ri.IsDeleted ?? false));

            if (existingIngredient == null)
            {
                throw new KeyNotFoundException("RecipeIngredient not found or deleted.");
            }

            // Update the fields
            existingIngredient.Quantity = recipeIngredient.Quantity;
            existingIngredient.QuantityTypeId = recipeIngredient.QuantityTypeId;

            // Save changes to the database
            _context.RecipeIngredients.Update(existingIngredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int recipeId, int ingredientId)
        {
            var recipeIngredient = await _context.RecipeIngredients
                .FirstOrDefaultAsync(ri => ri.RecipeId == recipeId 
                                           && ri.IngredientId == ingredientId 
                                           && !(ri.IsDeleted ?? false));

            if (recipeIngredient == null)
            {
                throw new KeyNotFoundException("RecipeIngredient not found or already deleted.");
            }

            // Perform soft delete
            recipeIngredient.IsDeleted = true;

            _context.RecipeIngredients.Update(recipeIngredient);
            await _context.SaveChangesAsync();
        }
    }

}