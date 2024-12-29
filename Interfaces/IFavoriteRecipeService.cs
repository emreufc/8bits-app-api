using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IFavoriteRecipeService
    {
        Task<FavoriteRecipeDto> AddFavoriteAsync(FavoriteRecipeDto dto);
        Task<bool> RemoveFavoriteAsync(int userId, int recipeId);
        Task<IEnumerable<FavoriteRecipeDto>> GetFavoritesByUserIdAsync(int userId);
    }
}
