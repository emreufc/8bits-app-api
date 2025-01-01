using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IIngredientRepository
    {
        Task<(IEnumerable<Ingredient> ingredients, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<Ingredient> GetByIdAsync(int id);
        Task<(IEnumerable<Ingredient>ingredients,int totalCount)> GetIngredientByCategoryAsync(List<string> selectedCategories, int pageNumber,int pageSize);
    }
}
