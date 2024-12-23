// using _8bits_app_api.Interfaces;
// using _8bits_app_api.Models;
//
// namespace _8bits_app_api.Services
// {
//     public class RecipeImageReadingService : IRecipeImagesReadingService
//     {
//         private readonly IRecipeImagesRepository _recipeImagesRepository;
//
//         public RecipeImageReadingService(IRecipeImagesRepository recipeImagesRepository)
//         {
//             _recipeImagesRepository = recipeImagesRepository;
//         }
//         public async Task<IEnumerable<RecipeImage>> GetAllRecipeImagesAsync()
//         {
//             return await _recipeImagesRepository.GetAllRecipeImagesAsync();
//         }
//         public async Task<RecipeImage> GetRecipeImageById(int id)
//         {
//             return await _recipeImagesRepository.GetRecipeImageById(id);
//         }
//     }
// }
