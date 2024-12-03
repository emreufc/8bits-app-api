using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class AllergyRepository : IAllergyRepository
    {
        private readonly mydbcontext _context;

        public AllergyRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Allergy>> GetAllAllergyAsync()
        {
            return await _context.Allergies.ToListAsync();
        }

        public async Task<Allergy> GetAllergyByIdAsync(int id)
        {
            return await _context.Allergies.FindAsync(id);
        }
    }
}
