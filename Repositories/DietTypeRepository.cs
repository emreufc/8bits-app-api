using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class DietTypeRepository : IDietTypeRepository
    {
        private readonly mydbcontext _context;

        public DietTypeRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<DietType> dietTypes, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.DietTypes.Where(p => p.IsDeleted == false).CountAsync();
            var dietTypes = await _context.DietTypes.Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (dietTypes, totalCount);
        }

        public async Task<DietType> GetByIdAsync(int id)
        {
            return await _context.DietTypes.FindAsync(id);
        }
    }

}
