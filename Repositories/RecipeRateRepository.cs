using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class RecipeRateRepository : IRecipeRateRepository
    {
        private readonly mydbcontext _mydbcontext;

        public RecipeRateRepository(mydbcontext mydbcontext)
        {
            _mydbcontext = mydbcontext;
        }
        public async Task<IEnumerable<RecipeRate>> GetAllRecipeRatesAsync()
        {
            return await _mydbcontext.RecipeRates.ToListAsync();
        }
        public async Task<RecipeRate> GetRecipeRateByIdAsync(int id)
        {
            return await _mydbcontext.RecipeRates.FindAsync(id);
        }
    }
}
