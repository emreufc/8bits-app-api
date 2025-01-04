using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IUserInventoryService
    {
        Task<bool> DeleteFromInventoryAsync(int inventoryId);
        Task<IEnumerable<InventoryDto>> GetInventoryByUserIdAsync(int userId);
        Task<UserInventory> AddToInventoryAsync(ShoppingListRequestDto inventory);
        Task<bool> UpdateInventoryAsync(int userId, ShoppingListRequestDto inventory);

        Task<UserInventory> AddOrUpdateInventoryItemAsync(ShoppingListRequestDto inventoryItemDto);
        Task<(bool IsSufficient, List<MissingIngredientDto> MissingIngredients)> IsInventorySufficientAsync(int recipeId, int userId);
        Task<bool> DeductIngredientsFromInventoryAsync(int recipeId, int userId);
    }
}
