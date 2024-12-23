using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IDietTypeRepository
    {
        Task<(IEnumerable<DietType> dietTypes, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<DietType> GetByIdAsync(int id);
    }

}