using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IAllergenRepository
    {
        Task<(IEnumerable<Allergen> allergens, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<Allergen> GetByIdAsync(int id);
    }

}