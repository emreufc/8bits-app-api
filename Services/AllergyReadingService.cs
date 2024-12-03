using _8bits_app_api.Models;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class AllergyReadingService : IAllergyReadingService
    {
        private readonly IAllergyRepository _allergyRepository;
        public AllergyReadingService(IAllergyRepository allergyRepository)
        {
            _allergyRepository = allergyRepository;
        }
        public async Task<IEnumerable<Allergy>> GetAllAllergyAsync()
        {
            return await _allergyRepository.GetAllAllergyAsync();
        }

        public async Task<Allergy> GetAllergyByIdAsync(int id)
        {
            return await _allergyRepository.GetAllergyByIdAsync(id);
        }

    }
}
