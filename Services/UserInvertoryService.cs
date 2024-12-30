using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class UserInvertoryService : IUserInventoryService
    {
        private readonly IUserInventoryRepository _repository;
        public UserInvertoryService(IUserInventoryRepository repository) {
            _repository = repository;
        }
        public async Task<UserInventory> AddToInventoryAsync(ShoppingListRequestDto inventory)
        {
            var existingInventory = await _repository.GetInventoryByUserIdAndIngredientIdAsync(inventory.UserId, inventory.IngredientId);

            if (existingInventory != null)
            {

                existingInventory.Quantity += inventory.Quantity;
                await _repository.UpdateInventoryAsync(existingInventory);

                return existingInventory;
            }
            var Userinventory = new UserInventory
            {
                UserId = inventory.UserId,
                IngredientId = inventory.IngredientId,
                QuantityTypeId = inventory.QuantityTypeId,
                Quantity = inventory.Quantity,
                IsDeleted = false // Varsayılan olarak false
            };

            return await _repository.AddToInventoryAsync(Userinventory);
        }

        public async Task<IEnumerable<UserInventory>> GetInventoryByUserIdAsync(int userId)
        {
            return await _repository.GetInventoryByUserIdAsync(userId);
        }
        public async Task<bool> DeleteFromInventoryAsync(int inventoryId)
        {
            return await _repository.DeleteFromInventoryAsync(inventoryId);
        }
        public async Task<bool> UpdateInventoryAsync(int userId, ShoppingListRequestDto inventory)
        {
            var userInventories = await _repository.GetInventoryByUserIdAsync(userId);
            var existingUserInventory = userInventories.FirstOrDefault(ui => ui.IngredientId == inventory.IngredientId);
            if (existingUserInventory == null)
            {
                return false;
            }

            existingUserInventory.Quantity = inventory.Quantity;

            // Update other properties as needed...

            return await _repository.UpdateInventoryAsync(existingUserInventory);
        }
    }
}
