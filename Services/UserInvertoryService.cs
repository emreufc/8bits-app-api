using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Services
{
    public class UserInvertoryService : IUserInventoryService
    {
        private readonly IConversionService _conversionservice;
        private readonly IUserInventoryRepository _repository;
        private readonly IRecipeIngredientRepository _recipeingredientRepository;
        public UserInvertoryService(IUserInventoryRepository repository,IConversionService conversionService,IRecipeIngredientRepository recipeIngredientRepository) {
            _repository = repository;
            _conversionservice = conversionService;
            _recipeingredientRepository = recipeIngredientRepository;
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
        public async Task<UserInventory> AddOrUpdateInventoryItemAsync(ShoppingListRequestDto inventoryItemDto)
        {
            // Mevcut öğeyi kontrol et
            var existingInventoryItem = await _repository.GetInventoryByUserIdAndIngredientIdAsync(inventoryItemDto.UserId, inventoryItemDto.IngredientId);

            if (existingInventoryItem != null)
            {
                // Mevcut ürünün miktarını artır
                existingInventoryItem.Quantity += inventoryItemDto.Quantity;
                await _repository.UpdateInventoryAsync(existingInventoryItem);
                return existingInventoryItem;
            }

            // Yeni bir ürün oluştur
            var newInventoryItem = new UserInventory
            {
                UserId = inventoryItemDto.UserId,
                IngredientId = inventoryItemDto.IngredientId,
                Quantity = inventoryItemDto.Quantity,
                QuantityTypeId = inventoryItemDto.QuantityTypeId
            };

            await _repository.AddToInventoryAsync(newInventoryItem);
            return newInventoryItem;
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
        public async Task<(bool IsSufficient, List<MissingIngredientDto> MissingIngredients)> IsInventorySufficientAsync(int recipeId, int userId)
        {
            // Tarif malzemelerini al
            var recipeIngredients = await _recipeingredientRepository.GetByIdAsync(recipeId);
            // Kullanıcının envanterini al
            var userInventory = await _repository.GetInventoryByUserIdAsync(userId);

            // Eksik malzemeleri tutacak liste
            var missingIngredients = new List<MissingIngredientDto>();
            var isSufficient = true;

            foreach (var ingredient in recipeIngredients)
            {
                // Envanterde malzeme kontrolü yap
                var inventoryItem = userInventory.FirstOrDefault(ui => ui.IngredientId == ingredient.IngredientId);

                if (inventoryItem == null)
                {
                    // Envanterde bulunmayan malzeme eksik
                    isSufficient = false;
                    missingIngredients.Add(new MissingIngredientDto
                    {
                        RecipeIngredientId = ingredient.RecipeIngredientId,
                        RecipeId = ingredient.RecipeId,
                        IngredientId = ingredient.IngredientId,
                        IngredientName = ingredient.Ingredient.IngredientName,
                        Unit = ingredient.QuantityType.QuantityTypeDesc,
                        Quantity = ingredient.Quantity,
                        QuantityTypeId = ingredient.QuantityTypeId,
                        IsDeleted = ingredient.IsDeleted ?? false
                    });
                }
                else
                {
                    // Envanterde varsa miktar kontrolü yap
                    var conversion = await _conversionservice.ConvertToStandardUnitAsync(
                        ingredient.IngredientId,
                        ingredient.QuantityTypeId,
                        ingredient.Quantity
                    );

                    if (inventoryItem.Quantity < conversion.ConvertedQuantity)
                    {
                        isSufficient = false;
                        var missingQuantity = conversion.ConvertedQuantity - inventoryItem.Quantity;
                        missingIngredients.Add(new MissingIngredientDto
                        {
                            RecipeIngredientId = ingredient.RecipeIngredientId,
                            RecipeId = ingredient.RecipeId,
                            IngredientId = ingredient.IngredientId,
                            IngredientName = ingredient.Ingredient.IngredientName,
                            Unit = ingredient.QuantityType.QuantityTypeDesc,
                            Quantity = missingQuantity,
                            QuantityTypeId = ingredient.QuantityTypeId,
                            IsDeleted = ingredient.IsDeleted ?? false
                        });
                    }
                }
            }

            return (isSufficient, missingIngredients);
        }

        public async Task<bool> DeductIngredientsFromInventoryAsync(int recipeId, int userId)
        {

            var recipeIngredients = await _recipeingredientRepository.GetByIdAsync(recipeId);

            foreach (var ingredient in recipeIngredients)
            {

                var inventoryItem = await _repository.GetInventoryByUserIdAndIngredientIdAsync(userId, ingredient.IngredientId);

                if (inventoryItem == null)
                {
                    throw new Exception($"Ingredient ID {ingredient.IngredientId} bulunamadı.");
                }


                var conversion = await _conversionservice.ConvertToStandardUnitAsync(
                    ingredient.IngredientId,
                    ingredient.QuantityTypeId,
                    ingredient.Quantity
                );

                if (inventoryItem.Quantity < conversion.ConvertedQuantity)
                {
                    throw new Exception($"Ingredient ID {ingredient.IngredientId} için yetersiz miktar.");
                }

                // Miktarı düş
                inventoryItem.Quantity -= conversion.ConvertedQuantity;
                await _repository.UpdateInventoryAsync(inventoryItem); // `await` ekleyerek asenkron bekleme yapın
            }

            return true;
        }




    }
}
