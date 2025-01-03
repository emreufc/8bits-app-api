using _8bits_app_api.Dtos;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            var recipeMatchDtos = recipesWithMatch.Select(recipe => new RecipeWithMatchDto
            {
                RecipeId = recipe.RecipeId,
                RecipeName = recipe.RecipeName,
                PersonCount = recipe.PersonCount,
                PreparationTime = recipe.PreparationTime,
                CookingTime = recipe.CookingTime,
                ImageUrl = recipe.ImageUrl,
                RecipeRate = recipe.RecipeRate,
                Vegan = recipe.Vegan,
                Vegetarian = recipe.Vegetarian,
                Pescatarian = recipe.Pescatarian,
                Keto = recipe.Keto,
                Paleo = recipe.Paleo,
                Mediterranean = recipe.Mediterranean,
                GlutenFree = recipe.GlutenFree,
                DairyFree = recipe.DairyFree,
                LowCarb = recipe.LowCarb,
                Flexitarian = recipe.Flexitarian,
                Normal = recipe.Normal,
                IsDeleted = recipe.IsDeleted,
                Kahvalti = recipe.Kahvalti,
                Oglen = recipe.Oglen,
                Aksam = recipe.Aksam,
                Tatli = recipe.Tatli,
                Icecek = recipe.Icecek,
                MatchPercentage = recipe.RecipeIngredients.Count == 0
            ? 0
            : recipe.RecipeIngredients.Count(ri =>
                  userInventory.Any(ui => ui.IngredientId == ri.IngredientId)) * 100.0
                  / recipe.RecipeIngredients.Count
            })
            .OrderByDescending(r => r.MatchPercentage)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            return (recipeMatchDtos, totalCount);
        }
        public async Task<(IEnumerable<Recipe> recipes, int totalCount)> GetFilteredRecipes(int userId, List<string> selectedCategories, int pageNumber, int pageSize)
        {
            var userDietPreferences = _context.DietPreferences
                .Where(dp => dp.UserId == userId && dp.IsDeleted == false)
                .Select(dp => dp.DietTypeId)
                .ToList();

            // Get the corresponding column names for the user's diet preferences
            var dietTypeColumns = _context.DietTypes
                .Where(dt => userDietPreferences.Contains(dt.DietTypeId))
                .Select(dt => dt.DietTypeName)
                .ToList();

            string dietFilter = string.Empty;
            if (dietTypeColumns.Any())
            {
                var dietConditions = dietTypeColumns.Select(column => $"[{column}] = 1").ToList();
                dietFilter = string.Join(" AND ", dietConditions);
            }

            string categoryFilter = string.Empty;
            if (selectedCategories != null && selectedCategories.Any())
            {
                var categoryConditions = selectedCategories.Select(category => $"[{category}] = 1").ToList();
                categoryFilter = string.Join(" AND ", categoryConditions);
            }

            // Combine diet and category filters
            string whereClause = string.Empty;
            if (!string.IsNullOrEmpty(dietFilter) && !string.IsNullOrEmpty(categoryFilter))
            {
                whereClause = $"{dietFilter} AND {categoryFilter}";
            }
            else if (!string.IsNullOrEmpty(dietFilter))
            {
                whereClause = dietFilter;
            }
            else if (!string.IsNullOrEmpty(categoryFilter))
            {
                whereClause = categoryFilter;
            }

            string query = $"SELECT * FROM Recipes";
            if (!string.IsNullOrEmpty(whereClause))
            {
                query += $" WHERE {whereClause}";
            }

            // Execute raw SQL query with pagination
            var totalCount = await _context.Recipes.FromSqlRaw(query).CountAsync();
            var pagedRecipes = await _context.Recipes
                .FromSqlRaw(query)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (pagedRecipes, totalCount);
        }

    }
}
