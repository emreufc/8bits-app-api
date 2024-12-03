using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IDietTypeReadingService
    {
        Task<IEnumerable<DietType>> GetAllDietTypeAsync();
        Task<DietType> GetDietTypeByIdAsync(int id);
    }
}
