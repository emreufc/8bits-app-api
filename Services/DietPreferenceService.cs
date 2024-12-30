using _8bits_app_api.Models;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Dtos;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class DietPreferenceReadingService : IDietPreferenceReadingService
    {
        private readonly IDietPreferenceRepository _dietPreferenceRepository;

        public DietPreferenceReadingService(IDietPreferenceRepository dietPreferenceRepository)
        {
            _dietPreferenceRepository = dietPreferenceRepository;
        }

        public async Task<(IEnumerable<DietPreference> dietPreferences, int totalCount)> GetDietPreferencesPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _dietPreferenceRepository.GetDietPreferencesPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<DietPreference> GetDietPreferenceByIdAsync(int id)
        {
            return await _dietPreferenceRepository.GetByIdAsync(id);
        }
        public async Task UpdateDietPreferencesAsync(int userId, List<int> newDietPreferenceIds)
        {
            if (newDietPreferenceIds == null || !newDietPreferenceIds.Any())
            {
                throw new ArgumentException("Diet preference list cannot be null or empty.");
            }

            await _dietPreferenceRepository.UpdateDietPreferencesAsync(userId, newDietPreferenceIds);
        }
    }

}
