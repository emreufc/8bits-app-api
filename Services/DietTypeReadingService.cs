using _8bits_app_api.Models;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class DietTypeReadingService : IDietTypeReadingService
    {
        private readonly IDietTypeRepository _dietTypeRepository;
        public DietTypeReadingService(IDietTypeRepository dietTypeRepository)
        {
            _dietTypeRepository = dietTypeRepository;
        }
        public async Task<IEnumerable<DietType>> GetAllDietTypeAsync()
        {
            return await _dietTypeRepository.GetAllDietTypeAsync();
        }

        public async Task<DietType> GetDietTypeByIdAsync(int id)
        {
            return await _dietTypeRepository.GetDietTypeByIdAsync(id);
        }

    }
}
