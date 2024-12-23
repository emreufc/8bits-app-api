using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public class AllergenReadingService : IAllergenReadingService
    {
        private readonly IAllergenRepository _allergenRepository;

        public AllergenReadingService(IAllergenRepository allergenRepository)
        {
            _allergenRepository = allergenRepository;
        }

        public async Task<(IEnumerable<Allergen> allergens, int totalCount)> GetAllergensPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _allergenRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<Allergen> GetAllergenByIdAsync(int id)
        {
            return await _allergenRepository.GetByIdAsync(id);
        }
    }

}