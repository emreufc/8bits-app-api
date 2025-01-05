using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class IngredientReadingService : IIngredientReadingService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientReadingService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients, int totalCount)> GetIngredientsPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _ingredientRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IngredientWithQuantitiesDto> GetIngredientByIdAsync(int id)
        {
            return await _ingredientRepository.GetByIdAsync(id);
        }
        
        public async Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients, int totalCount)> SearchIngredientsAsync(string keyword, int pageNumber, int pageSize)
        {
            return await _ingredientRepository.SearchIngredientsAsync(keyword, pageNumber, pageSize);
        }

        public async Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients, int totalCount)> GetIngredientByCategoryAsync(List<string> selectedCategories, int pageNumber, int pageSize)
        {
            return await _ingredientRepository.GetIngredientByCategoryAsync(selectedCategories, pageNumber, pageSize) ;
        }

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            await _ingredientRepository.AddIngredientAsync(ingredient);
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            await _ingredientRepository.UpdateIngredientAsync(ingredient);
        }

        public async Task DeleteIngredientAsync(int id)
        {
            await _ingredientRepository.DeleteIngredientAsync(id);
        }
    }

}
