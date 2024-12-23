using _8bits_app_api.Models;
using _8bits_app_api.Interfaces;

namespace _8bits_app_api.Services
{
    public class DietTypeReadingService : IDietTypeReadingService
    {
        private readonly IDietTypeRepository _dietTypeRepository;

        public DietTypeReadingService(IDietTypeRepository dietTypeRepository)
        {
            _dietTypeRepository = dietTypeRepository;
        }

        public async Task<(IEnumerable<DietType> dietTypes, int totalCount)> GetDietTypesPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _dietTypeRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<DietType> GetDietTypeByIdAsync(int id)
        {
            return await _dietTypeRepository.GetByIdAsync(id);
        }
    }

}
