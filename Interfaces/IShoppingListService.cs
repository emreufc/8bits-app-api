using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IShoppingListService
    {
        Task<bool> DeleteFromShoppingListAsync(int shoppingListId);

        Task<ShoppingList> AddToShoppingListAsync(ShoppingListRequestDto shoppingList);
        Task<IEnumerable<ShoppingList>> GetShoppingListByUserIdAsync(int userId);
    }
}
