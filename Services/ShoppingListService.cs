using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public ShoppingListService(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository;
        }

        public async Task<bool> DeleteFromShoppingListAsync(int shoppingListId)
        {
            return await _shoppingListRepository.DeleteFromShoppingListAsync(shoppingListId);
        }
        public async Task<ShoppingList> AddToShoppingListAsync(ShoppingListRequestDto shoppingListDto)
        {
            var existingShoppingList = await _shoppingListRepository.GetByUserIdAndIngredientIdAsync(shoppingListDto.UserId, shoppingListDto.IngredientId);

            if (existingShoppingList != null)
            {
                existingShoppingList.Quantity = (int.Parse(existingShoppingList.Quantity) + shoppingListDto.Quantity).ToString();
                return await _shoppingListRepository.UpdateShoppingListAsync(existingShoppingList);
            }
            var shoppingList = new ShoppingList
            {
                UserId = shoppingListDto.UserId,
                IngredientId = shoppingListDto.IngredientId,
                QuantityTypeId = shoppingListDto.QuantityTypeId,
                Quantity = shoppingListDto.Quantity.ToString(),
                IsDeleted = false // Varsayılan olarak false
            };

            return await _shoppingListRepository.AddToShoppingListAsync(shoppingList);
        }
        public async Task<ShoppingList?> GetShoppingListItemByIdAndIngredientIdAsync(int userId, int ingredientId)
        {
            // Repository'den veriyi al
            return await _shoppingListRepository.GetByUserIdAndIngredientIdAsync(userId, ingredientId);
        }
        public async Task<IEnumerable<ShoppingListResponseDto>> GetShoppingListByUserIdAsync(int userId)
        {
            return await _shoppingListRepository.GetShoppingListByUserIdAsync(userId);
        }

    }
}
