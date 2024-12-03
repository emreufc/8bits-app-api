using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IIngredientReadingService
    {
        Task<IEnumerable<Ingredient>> GetAllIngredientAsync();
        Task<Ingredient> GetIngredientByIdAsync(int id);
    }
}
