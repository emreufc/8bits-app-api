using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class AllergenRepository : IAllergenRepository
    {
        private readonly mydbcontext _context;

        public AllergenRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Allergen> allergens, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Allergens.Where(p => p.IsDeleted == false).CountAsync();
            var allergens = await _context.Allergens
                .Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (allergens, totalCount);
        }

        public async Task<Allergen> GetByIdAsync(int id)
        {
            return await _context.Allergens.FindAsync(id);
        }
    }

}
