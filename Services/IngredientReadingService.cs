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

        public async Task<(IEnumerable<Ingredient> ingredients, int totalCount)> GetIngredientsPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _ingredientRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _ingredientRepository.GetByIdAsync(id);
        }
    }

}
