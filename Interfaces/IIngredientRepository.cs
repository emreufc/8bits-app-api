using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllIngredientAsync();
        Task<Ingredient> GetIngredientByIdAsync(int id);
    }
}
