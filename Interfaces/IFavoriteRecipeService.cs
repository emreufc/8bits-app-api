using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IFavoriteRecipeService
    {
        Task<FavoriteRecipeDto> AddFavoriteAsync(FavoriteRecipeDto dto);
        Task<bool> RemoveFavoriteAsync(int userId, int recipeId);
        Task<IEnumerable<Recipe>> GetFavoritesByUserIdAsync(int userId);
        Task<bool> IsUserFavouriteAsync(int userId, int recipeId);
    }
}
