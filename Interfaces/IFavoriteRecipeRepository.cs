using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IFavoriteRecipeRepository
    {
        Task<FavoriteRecipe> AddFavoriteAsync(FavoriteRecipe favoriteRecipe);
        Task<bool> RemoveFavoriteAsync(int userId, int recipeId);
        Task<IEnumerable<Recipe>> GetFavoritesByUserIdAsync(int userId);
    }
}
