using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IIngredientReadingService
    {
        Task<(IEnumerable<Ingredient> ingredients, int totalCount)> GetIngredientsPaginatedAsync(int pageNumber, int pageSize);
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task<(IEnumerable<Ingredient> ingredients, int totalCount)> GetIngredientByCategoryAsync(List<string> selectedCategories, int pageNumber, int pageSize);

    }

}
