using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IAllergyReadingService
    {
        Task<IEnumerable<Allergy>> GetAllAllergyAsync();
        Task<Allergy> GetAllergyByIdAsync(int id);
    }
}
