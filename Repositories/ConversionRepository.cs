using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class ConversionRepository : IConversionRepository
    {
        private readonly mydbcontext _context;

        public ConversionRepository(mydbcontext mydbcontext)
        {
            _context = mydbcontext;
        }
        public async Task<IEnumerable<IngredientConversion>> GetConversionsByIngredientIdAsync(int ingredientId)
        {
            return await _context.IngredientConversions
                        .Where(c => c.IngredientId == ingredientId)
                        .ToListAsync();
        }
        public async Task<IngredientConversion?> GetConversionbyQuantitytypeIdAsync(int ingredientId, int quantityTypeId)
        {
            return await _context.IngredientConversions
                .FirstOrDefaultAsync(c => c.IngredientId == ingredientId && c.QuantityTypeId == quantityTypeId);
        }
    }
}
