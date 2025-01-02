using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;
namespace _8bits_app_api.Repositories
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly mydbcontext _context;

        public ShoppingListRepository(mydbcontext context)
        {
            _context = context;
        }
        public async Task<ShoppingList> GetShoppingListByIdAsync(int shoppingListId)
        {
            return await _context.ShoppingLists.FirstOrDefaultAsync(s => s.ShoppingListId == shoppingListId && !s.IsDeleted == false);
        }

        public async Task<bool> DeleteFromShoppingListAsync(int shoppingListId)
        {
            var item = await _context.ShoppingLists.FindAsync(shoppingListId);
            if (item == null)
            {
                return false;
            }
            _context.ShoppingLists.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<ShoppingList> AddToShoppingListAsync(ShoppingList shoppingList)
        {
            await _context.ShoppingLists.AddAsync(shoppingList);
            await _context.SaveChangesAsync();
            return shoppingList;
        }

        public async Task<IEnumerable<ShoppingListResponseDto>> GetShoppingListByUserIdAsync(int userId)
        {
            return await _context.ShoppingLists
                .Where(s => s.UserId == userId && !(s.IsDeleted ?? false)) // Kullanıcıya ait ve silinmemiş veriler
                .Select(s => new ShoppingListResponseDto
                {
                    ShoppingListId = s.ShoppingListId,
                    UserId = s.UserId,
                    IngredientId = s.IngredientId,
                    QuantityTypeId = s.QuantityTypeId,
                    Quantity = s.Quantity,
                    IngredientName = s.Ingredient != null ? s.Ingredient.IngredientName : null,
                    IngImgUrl = s.Ingredient != null ? s.Ingredient.IngImgUrl : null,
                    QuantityTypeDesc = s.QuantityType != null ? s.QuantityType.QuantityTypeDesc : null
                })
                .ToListAsync();
        }
    }

}
