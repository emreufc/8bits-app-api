using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class DietPreferenceRepository : IDietPreferenceRepository
    {
        private readonly mydbcontext _context;

        public DietPreferenceRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<DietPreference> dietPreferences, int totalCount)> GetDietPreferencesPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.DietPreferences.Where(p => p.IsDeleted == false).CountAsync();
            var dietPreferences = await _context.DietPreferences.Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (dietPreferences, totalCount);
        }
        
        public async Task<(IEnumerable<DietPreference> dietPreferences, int totalCount)> GetDietPreferencesByUserAsync(int pageNumber, int pageSize, int userId)
        {
            var totalCount = await _context.DietPreferences.Where(p => p.IsDeleted == false && p.UserId == userId).CountAsync();
            var dietPreferences = await _context.DietPreferences.Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (dietPreferences, totalCount);
        }

        public async Task<DietPreference> GetByIdAsync(int id)
        {
            return await _context.DietPreferences.FindAsync(id);
        }

        //public async Task UpdateShoppingListAsync(ShoppingList shoppingList)
        //{
        //    _context.ShoppingLists.Update(shoppingList);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<DietPreference> AddToDietPreferenceAsync(DietPreference dietPreference)
        {
            await _context.DietPreferences.AddAsync(dietPreference);
            await _context.SaveChangesAsync();
            return dietPreference;
        }
        public async Task<DietPreference> GetDietPreferenceByIdAsync(int dietPreferenceId)
        {
            return await _context.DietPreferences.FindAsync(dietPreferenceId);
        }

        public async Task UpdateDietPreferencesAsync(int userId, List<int> newDietPreferenceIds)
        {
            // Tablodaki mevcut kullanıcı tercihlerini alıyoruz.
            var existingPreferences = await _context.DietPreferences
                .Where(dp => dp.UserId == userId && dp.IsDeleted == false)
                .ToListAsync();

            // Yeni gelen ID'lerden mevcut olmayanları ekle.
            var preferencesToAdd = newDietPreferenceIds
                .Where(id => !existingPreferences.Any(ep => ep.DietTypeId == id))
                .Select(id => new DietPreference
                {
                    UserId = userId,
                    DietTypeId = id,
                    IsDeleted = false
                });

            await _context.DietPreferences.AddRangeAsync(preferencesToAdd);

            // Mevcut tablodaki ama listede olmayanları sil.
            var preferencesToDelete = existingPreferences
                .Where(ep => !newDietPreferenceIds.Contains(ep.DietTypeId.Value))
                .ToList();

            foreach (var pref in preferencesToDelete)
            {
                pref.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
        }
    }

}
