using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IUserInventoryRepository
    {
        Task<UserInventory> GetInventoryByUserIdAndIngredientIdAsync(int userId, int? ingredientId);
        Task<bool> UpdateInventoryAsync(UserInventory inventory);
        Task<bool> DeleteFromInventoryAsync(int inventory_id);
        Task<IEnumerable<UserInventory>> GetInventoryByUserIdAsync(int userId);
        Task<UserInventory> AddToInventoryAsync(UserInventory inventory);

    }
}
