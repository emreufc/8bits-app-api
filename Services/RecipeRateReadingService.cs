// using _8bits_app_api.Interfaces;
// using _8bits_app_api.Models;
//
// namespace _8bits_app_api.Services
// {
//     public class RecipeRateReadingService : IRecipeRateReadingService
//     {
//         private readonly IRecipeRateRepository _recipeRateRepository;
//
//         public RecipeRateReadingService(IRecipeRateRepository recipeRatesRepository)
//         {
//             _recipeRateRepository = recipeRatesRepository;
//         }
//         public async Task<IEnumerable<RecipeRate>> GetAllRecipeRatesAsync()
//         {
//             return await _recipeRateRepository.GetAllRecipeRatesAsync();
//         }
//         public async Task<RecipeRate> GetRecipeRateByIdAsync(int id)
//         {
//             return await _recipeRateRepository.GetRecipeRateByIdAsync(id);
//         }
//     }
// }
