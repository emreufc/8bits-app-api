using _8bits_app_api.Models;

namespace _8bits_app_api.Repositories
{
    public interface IDietTypeRepository
    {
        Task<IEnumerable<DietType>> GetAllDietTypeAsync();
        Task<DietType> GetDietTypeByIdAsync(int id);
    }
}