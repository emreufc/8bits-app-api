using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class IngredientRepository :  IIngredientRepository
    {
        private readonly mydbcontext _context;

        public IngredientRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredientAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _context.Ingredients.FindAsync(id);
        }
    }
}
