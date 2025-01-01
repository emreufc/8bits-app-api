﻿using _8bits_app_api.Interfaces;
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

        public async Task<(IEnumerable<Ingredient> ingredients, int totalCount)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Ingredients.Where(p => p.IsDeleted == false).CountAsync();
            var ingredients = await _context.Ingredients.Where(p => p.IsDeleted == false)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (ingredients, totalCount);
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            return await _context.Ingredients.FindAsync(id);
        }

        public async Task<(IEnumerable<Ingredient> ingredients, int totalCount)> GetIngredientByCategoryAsync(List<string> selectedCategories, int pageNumber, int pageSize)
        {
            // Base query for filtering by categories
            var query = _context.Ingredients.Where(ingredient => ingredient.IsDeleted ==false && selectedCategories.Contains(ingredient.IngredientCategory));

            // Get the total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var ingredients = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (ingredients, totalCount);
        }
    }

}
