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

        public async Task<IEnumerable<ShoppingList>> GetShoppingListByUserIdAsync(int userId)
        {
            return await _shoppingListRepository.GetShoppingListByUserIdAsync(userId);
        }
    }
}
