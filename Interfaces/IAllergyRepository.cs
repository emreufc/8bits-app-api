using _8bits_app_api.Models;

namespace _8bits_app_api.Repositories
{
    public interface IAllergyRepository
    {
        Task<IEnumerable<Allergy>> GetAllAllergyAsync();
        Task<Allergy> GetAllergyByIdAsync(int id);
    }
}