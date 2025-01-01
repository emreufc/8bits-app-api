﻿using _8bits_app_api.Dtos;
using _8bits_app_api.Models;

namespace _8bits_app_api.Services
{
    public interface IIngredientReadingService
    {
        Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients, int totalCount)> GetIngredientsPaginatedAsync(int pageNumber, int pageSize);
        Task<IngredientWithQuantitiesDto> GetIngredientByIdAsync(int id);
        Task<(IEnumerable<IngredientWithQuantitiesDto> ingredients, int totalCount)> GetIngredientByCategoryAsync(List<string> selectedCategories, int pageNumber, int pageSize);

    }

}
