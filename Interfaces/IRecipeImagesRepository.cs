using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IRecipeImagesRepository
    {
        Task<IEnumerable<RecipeImage>> GetAllRecipeImagesAsync();
        Task<RecipeImage> GetRecipeImageById(int id);
    }
}
