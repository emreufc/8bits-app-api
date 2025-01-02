using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Repositories;

namespace _8bits_app_api.Services
{
    public class UserInvertoryService : IUserInventoryService
    {
        private readonly IConversionService _conversionservice;
        private readonly IUserInventoryRepository _repository;
        public UserInvertoryService(IUserInventoryRepository repository,IConversionService conversionService) {
            _repository = repository;
            _conversionservice = conversionService;
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

        public async Task<IEnumerable<InventoryDto>> GetInventoryByUserIdAsync(int userId)
        {
            var inventories = await _repository.GetInventoryByUserIdAsync(userId);

            var inventoryDtos = inventories.Select(i => new InventoryDto
            {
                InventoryId = i.InventoryId,
                UserId = userId,
                IngredientId = i.Ingredient.IngredientId,
                IngredientName = i.Ingredient?.IngredientName,
                IngredientCategory = i.Ingredient?.IngredientCategory,
                IngredientImageUrl = i.Ingredient?.IngImgUrl,
                Quantity = i.Quantity.Value,
                QuantityTypeDesc = i.QuantityType?.QuantityTypeDesc,
                IsDeleted = i.IsDeleted ?? false
            }).ToList();

            return inventoryDtos;
        }
        public async Task<bool> DeleteFromInventoryAsync(int inventoryId)
        {
            return await _repository.DeleteFromInventoryAsync(inventoryId);
        }
        public async Task<bool> UpdateInventoryAsync(int userId, ShoppingListRequestDto inventory)
        {
            // Kullanıcının envanterini al
            var userInventories = await _repository.GetInventoryByUserIdAsync(userId);

            // Güncellenmesi gereken envanteri bul
            var existingUserInventory = userInventories.FirstOrDefault(ui => ui.IngredientId == inventory.IngredientId);

            if (existingUserInventory == null)
            {
                return false; // Eğer envanter yoksa false döndür
            }

            // Conversion işlemini yap
            var conversionResult = await _conversionservice.ConvertToStandardUnitAsync(inventory.IngredientId, inventory.QuantityTypeId, inventory.Quantity);
            if (conversionResult == null)
            {
                throw new Exception("Conversion information not found for the given ingredient and quantity type.");
            }

            // Envanteri güncelle
            existingUserInventory.Quantity = conversionResult.ConvertedQuantity;
            existingUserInventory.QuantityTypeId = conversionResult.Unit == "ml" ? 45 : 5; // Birim türünü güncelle

            return await _repository.UpdateInventoryAsync(existingUserInventory); // Envanteri güncelle ve sonucu döndür
        }

    }
}
