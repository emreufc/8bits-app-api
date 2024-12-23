using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IAllergenReadingService
    {
        Task<(IEnumerable<Allergen> allergens, int totalCount)> GetAllergensPaginatedAsync(int pageNumber, int pageSize);
        Task<Allergen> GetAllergenByIdAsync(int id);
    }

}