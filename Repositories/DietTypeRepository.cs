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

        public async Task<IEnumerable<DietType>> GetAllDietTypeAsync()
        {
            return await _context.DietTypes.ToListAsync();
        }

        public async Task<DietType> GetDietTypeByIdAsync(int id)
        {
            return await _context.DietTypes.FindAsync(id);
        }
    }
}
