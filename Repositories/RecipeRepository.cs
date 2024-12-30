using _8bits_app_api.Dtos;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly mydbcontext _context;

        public RecipeRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Recipe> recipes, int totalCount)> GetAllRecipesAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Recipes.Where(p => p.IsDeleted == false).CountAsync();
            var recipes = await _context.Recipes
                .Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (recipes, totalCount);
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task<(IEnumerable<RecipeWithMatchDto> recipes, int totalCount)> GetAllRecipesWithMatchAsync(int userId, int pageNumber, int pageSize)
        {
            // Kullanıcının envanterini bellek içine al
            var userInventory = await _context.UserInventories
                .Where(ui => ui.UserId == userId)
                .ToListAsync();

            // Toplam tarif sayısını al
            var totalCount = await _context.Recipes
                .Where(r => r.IsDeleted == false)
                .CountAsync();

            // Tarifleri al ve eşleşme oranını hesapla
            var recipesWithMatch = await _context.Recipes
                .Where(r => r.IsDeleted == false)
                .Include(r => r.RecipeIngredients) // Tarif malzemelerini dahil et
                .ToListAsync(); // Bellek içine al

            var recipeMatchDtos = recipesWithMatch
                .Select(recipe => new RecipeWithMatchDto
                {
                    RecipeId = recipe.RecipeId,
                    RecipeName = recipe.RecipeName,
                    MatchPercentage = recipe.RecipeIngredients.Count == 0
                        ? 0
                        : recipe.RecipeIngredients.Count(ri =>
                              userInventory.Any(ui => ui.IngredientId == ri.IngredientId)) * 100.0
                              / recipe.RecipeIngredients.Count
                })
                .OrderByDescending(r => r.MatchPercentage) // Eşleşme oranına göre sıralama
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (recipeMatchDtos, totalCount);
        }


    }
}
