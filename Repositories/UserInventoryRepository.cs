using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class UserInventoryRepository : IUserInventoryRepository
    {
        private readonly mydbcontext _context;

        public UserInventoryRepository(mydbcontext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserInventory>> GetInventoryByUserIdAsync(int userId)
        {
            return await _context.UserInventories
               .Include(ui => ui.QuantityType)
               .Include(ui => ui.Ingredient)
               .Where(ui => ui.UserId == userId)
               .ToListAsync();
        }

        public async Task<bool> UpdateInventoryAsync(UserInventory inventory)
        {
            _context.UserInventories.Update(inventory);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<UserInventory> GetInventoryByUserIdAndIngredientIdAsync(int userId, int? ingredientId) { 
            return await _context.UserInventories.FirstOrDefaultAsync(ui => ui.UserId == userId && ui.IngredientId == ingredientId);
        }

        public async Task<UserInventory> AddToInventoryAsync(UserInventory inventory)
        {
            await _context.UserInventories.AddAsync(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<IEnumerable<UserInventory>> GetInventoryByCategoryAsync(int userId, List<string> selectedCategories)
        {
            return await _context.UserInventories
                .Include(ui => ui.QuantityType)
                .Include(ui => ui.Ingredient)
                .Where(ui => ui.UserId == userId && 
                             ui.Ingredient != null && 
                             (selectedCategories == null || !selectedCategories.Any() || selectedCategories.Contains(ui.Ingredient.IngredientCategory)))
                .ToListAsync();
        }

        public async Task<bool> DeleteFromInventoryAsync(int inventory_id)
        {
            var item = await _context.UserInventories.FindAsync(inventory_id);
            if (item == null)
            {
                return false;
            }
            _context.UserInventories.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}
