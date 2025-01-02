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
    }
}
