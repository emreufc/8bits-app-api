using _8bits_app_api.Dtos;
using _8bits_app_api.Models;
using _8bits_app_api.Dtos;

namespace _8bits_app_api.Interfaces
{
    public interface IDietPreferenceReadingService
    {
        Task<(IEnumerable<DietPreference> dietPreferences, int totalCount)> GetDietPreferencesPaginatedAsync(int pageNumber, int pageSize);
        Task<(IEnumerable<DietPreference> dietPreferences, int totalCount)> GetDietPreferencesByUserAsync(int pageNumber, int pageSize, int userId);
        Task<DietPreference> GetDietPreferenceByIdAsync(int id);
        Task UpdateDietPreferencesAsync(int userId, List<int> newDietPreferenceIds);
    }
}
