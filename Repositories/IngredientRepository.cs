using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly mydbcontext _context;

        public IngredientRepository(mydbcontext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients, int totalCount)> SearchIngredientsAsync(string keyword, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return (Enumerable.Empty<IngredientWithQuantitiesDto>(), 0);

            keyword = keyword.ToLower(); // Anahtar kelimeyi küçük harfe çevirerek case-insensitive arama yapıyoruz.

            var query = _context.Ingredients
                .Where(i => i.IsDeleted == false && 
                            (i.IngredientName.ToLower().Contains(keyword) || i.IngredientCategory.ToLower().Contains(keyword)));

            // Toplam sonuç sayısını alıyoruz.
            var totalCount = await query.CountAsync();

            // Sayfalanmış malzemeleri alıyoruz.
            var ingredients = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ingredient => new IngredientWithQuantitiesDto
                {
                    Ingredient = ingredient,
                    QuantityTypeIds = _context.RecipeIngredients
                        .Where(ri => ri.IngredientId == ingredient.IngredientId)
                        .Select(ri => ri.QuantityTypeId)
                        .Distinct()
                        .ToList(),
                    QuantityTypes = _context.RecipeIngredients
                        .Where(ri => ri.IngredientId == ingredient.IngredientId)
                        .Join(
                            _context.QuantityTypes,
                            ri => ri.QuantityTypeId,
                            qt => qt.QuantityTypeId,
                            (ri, qt) => qt.QuantityTypeDesc
                        )
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            return (ingredients, totalCount);
        }

        
        public async Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Ingredients.Where(p => p.IsDeleted == false).CountAsync();

            // Fetch paginated ingredients with their distinct quantity_type_id values
            var ingredients = await _context.Ingredients
                .Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ingredient => new IngredientWithQuantitiesDto
                {
                    Ingredient = ingredient,
                    QuantityTypeIds = _context.RecipeIngredients
                        .Where(ri => ri.IngredientId == ingredient.IngredientId)
                        .Select(ri => ri.QuantityTypeId)
                        .Distinct()
                        .ToList(),
                    QuantityTypes = _context.RecipeIngredients
                        .Where(ri => ri.IngredientId == ingredient.IngredientId)
                        .Join(
                            _context.QuantityTypes,
                            ri => ri.QuantityTypeId,
                            qt => qt.QuantityTypeId,
                            (ri, qt) => qt.QuantityTypeDesc
                        )
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            return (ingredients, totalCount);
        }

        public async Task<IngredientWithQuantitiesDto> GetByIdAsync(int id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.IngredientId == id && i.IsDeleted==false);

            if (ingredient == null)
            {
                return null;
            }

            var ingredientWithQuantities = new IngredientWithQuantitiesDto
            {
                Ingredient = ingredient,
                QuantityTypeIds = _context.RecipeIngredients
                    .Where(ri => ri.IngredientId == ingredient.IngredientId)
                    .Select(ri => ri.QuantityTypeId)
                    .Distinct()
                    .ToList(),
                QuantityTypes = _context.RecipeIngredients
                    .Where(ri => ri.IngredientId == ingredient.IngredientId)
                    .Join(
                        _context.QuantityTypes,
                        ri => ri.QuantityTypeId,
                        qt => qt.QuantityTypeId,
                        (ri, qt) => qt.QuantityTypeDesc
                    )
                    .Distinct()
                    .ToList()
            };

            return ingredientWithQuantities;
        }

        public async Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients, int totalCount)> GetIngredientByCategoryAsync(List<string> selectedCategories, int pageNumber, int pageSize)
        {
            var query = _context.Ingredients.Where(ingredient => ingredient.IsDeleted == false && selectedCategories.Contains(ingredient.IngredientCategory));

            // Get the total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination and fetch ingredients with their related quantity details
            var ingredients = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ingredient => new IngredientWithQuantitiesDto
                {
                    Ingredient = ingredient,
                    QuantityTypeIds = _context.RecipeIngredients
                        .Where(ri => ri.IngredientId == ingredient.IngredientId)
                        .Select(ri => ri.QuantityTypeId)
                        .Distinct()
                        .ToList(),
                    QuantityTypes = _context.RecipeIngredients
                        .Where(ri => ri.IngredientId == ingredient.IngredientId)
                        .Join(
                            _context.QuantityTypes,
                            ri => ri.QuantityTypeId,
                            qt => qt.QuantityTypeId,
                            (ri, qt) => qt.QuantityTypeDesc
                        )
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            return (ingredients, totalCount);
        }

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            var maxId = await _context.Ingredients
                .Where(i => !(i.IsDeleted ?? false))
                .MaxAsync(i => (int?)i.IngredientId) ?? 0;

            // Set the new IngredientId
            ingredient.IngredientId = maxId + 1;
            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await GetByIdAsync(id);
            if (ingredient != null)
            {
                ingredient.Ingredient.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }

}
