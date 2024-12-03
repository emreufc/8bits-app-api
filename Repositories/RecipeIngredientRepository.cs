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

        public async Task<IEnumerable<RecipeIngredient>> GetAllRecipeIngredientAsync()
        {
            return await _context.RecipeIngredients.ToListAsync();
        }

        public async Task<RecipeIngredient> GetRecipeIngredientByIdAsync(int id)
        {
            return await _context.RecipeIngredients.FindAsync(id);
        }
        public async Task<IEnumerable<RecipeIngredient>> GetRecipeIngredientsByRecipeIdAsync(int recipeId)
        {
            return await _context.RecipeIngredients
                                 .Where(ri => ri.RecipeId == recipeId)
                                 .OrderBy(ri => ri.IngredientId) // Opsiyonel: Belirli bir sıraya göre sıralamak için
                                 .ToListAsync();
        }

    }
}