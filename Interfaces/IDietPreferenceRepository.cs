using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IDietPreferenceRepository
    {
        Task<(IEnumerable<DietPreference> dietPreferences, int totalCount)> GetDietPreferencesPaginatedAsync(int pageNumber, int pageSize);
        Task<DietPreference> GetByIdAsync(int id);
        Task<DietPreference> AddToDietPreferenceAsync(DietPreference dietPreference);
        Task<DietPreference> GetDietPreferenceByIdAsync(int dietPreferenceID);
        Task UpdateDietPreferencesAsync(int userId, List<int> newDietPreferenceIds);
    }
}
