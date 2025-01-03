using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IShoppingListRepository
    {
        Task<ShoppingList> GetShoppingListByIdAsync(int shoppingListId);
        Task<bool> DeleteFromShoppingListAsync(int shoppingListId);
        Task<ShoppingList> AddToShoppingListAsync(ShoppingList shoppingList);
        Task<IEnumerable<ShoppingListResponseDto>> GetShoppingListByUserIdAsync(int userId);
        Task<ShoppingList?> GetByUserIdAndIngredientIdAsync(int userId, int ingredientId);
        Task<ShoppingList> UpdateShoppingListAsync(ShoppingList shoppingList);
    }
}
