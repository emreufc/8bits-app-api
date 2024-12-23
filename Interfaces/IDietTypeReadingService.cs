using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IDietTypeReadingService
    {
        Task<(IEnumerable<DietType> dietTypes, int totalCount)> GetDietTypesPaginatedAsync(int pageNumber, int pageSize);
        Task<DietType> GetDietTypeByIdAsync(int id);
    }
}
