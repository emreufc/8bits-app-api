using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface IShoppingListRepository
    {
        Task<ShoppingList> GetShoppingListByIdAsync(int shoppingListId);
        Task UpdateShoppingListAsync(ShoppingList shoppingList);
        Task<ShoppingList> AddToShoppingListAsync(ShoppingList shoppingList);
        Task<IEnumerable<ShoppingList>> GetShoppingListByUserIdAsync(int userId);
    }
}
