using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IRecipeImagesReadingService
    {
        Task<IEnumerable<RecipeImage>> GetAllRecipeImagesAsync();
        Task<RecipeImage> GetRecipeImageById(int id);
    }
}
