using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IIngredientRepository
    {
        Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize);
        Task<IngredientWithQuantitiesDto> GetByIdAsync(int id);
        Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients,int totalCount)> GetIngredientByCategoryAsync(List<string> selectedCategories, int pageNumber,int pageSize);
        Task AddIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(int id);
    }
}
