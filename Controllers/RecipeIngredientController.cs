﻿using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly IRecipeIngredientReadingService _recipeIngredientService;

        public RecipeIngredientController(IRecipeIngredientReadingService recipeIngredientService)
        {
            _recipeIngredientService = recipeIngredientService;
        }

        // GET: api/RecipeIngredient
        [HttpGet]
        public async Task<IActionResult> GetRecipeIngredients([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Page number and page size must be greater than 0.",
                    data = (object)null
                });
            }

            var (recipeIngredients, totalCount) = await _recipeIngredientService.GetRecipeIngredientsPaginatedAsync(pageNumber, pageSize);
            if (recipeIngredients == null || !recipeIngredients.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No recipe ingredients found in the database. Please ensure that recipe ingredients have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {recipeIngredients.Count()} recipe ingredients from the database.",
                data = recipeIngredients,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        // GET: api/RecipeIngredient/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeIngredient(int id)
        {
            var recipeIngredient = await _recipeIngredientService.GetRecipeIngredientByIdAsync(id);
            if (recipeIngredient == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No recipe ingredient found with ID {id}. Please check the ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Recipe ingredient with ID {id} retrieved successfully.",
                data = recipeIngredient
            });
        }
        // GET: api/RecipeIngredient/recipe/{recipeId}
        [HttpGet("recipe/{recipeId}")]
        public async Task<IActionResult> GetRecipeIngredientsByRecipeId(int recipeId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Page number and page size must be greater than 0.",
                    data = (object)null
                });
            }

            var (recipeIngredients, totalCount) = await _recipeIngredientService.GetRecipeIngredientsByRecipeIdPaginatedAsync(recipeId, pageNumber, pageSize);

            if (recipeIngredients == null || !recipeIngredients.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No recipe ingredients found with Recipe ID {recipeId}. Please check the Recipe ID and try again.",
                    data = (object)null
                });
            }

            // IngredientName bilgisi ile birlikte dön
            var data = recipeIngredients.Select(ri => new
            {
                ri.RecipeIngredientId,
                ri.RecipeId,
                ri.IngredientId,
                IngredientName = ri.Ingredient.IngredientName, // Ingredient ilişkisini kontrol et
                Unit =ri.QuantityType.QuantityTypeDesc,
                ri.Quantity,
                ri.QuantityTypeId,
                ri.IsDeleted
            });

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {recipeIngredients.Count()} ingredients for Recipe ID {recipeId}.",
                data = data,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

    }
}
