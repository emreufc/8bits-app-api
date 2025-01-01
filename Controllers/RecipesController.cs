using _8bits_app_api.Controllers;
using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : BaseController
    {
        private readonly IRecipeReadingService _recipeReadingService;

        public RecipesController(IRecipeReadingService recipeReadingService)
        {
            _recipeReadingService = recipeReadingService;
        }
        [HttpGet("recipes-with-match")]
        public async Task<IActionResult> GetRecipesWithMatch([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = GetCurrentUserId();

            if (userId <= 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Invalid user ID.",
                    data = (object)null
                });
            }

            var (recipes, totalCount) = await _recipeReadingService.GetAllRecipesWithMatchAsync(userId, pageNumber, pageSize);

            if (recipes == null || !recipes.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No recipes found.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = "Recipes retrieved successfully.",
                data = recipes,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<IActionResult> GetRecipes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
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

            var (recipes, totalCount) = await _recipeReadingService.GetAllRecipesAsync(pageNumber, pageSize);
            if (recipes == null || !recipes.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No recipes found in the database. Please ensure that recipes have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {recipes.Count()} recipes from the database.",
                data = recipes,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _recipeReadingService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No recipe found with ID {id}. Please check the ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Recipe with ID {id} retrieved successfully.",
                data = recipe
            });
        }
        [HttpGet("filtered")]
        public async Task<IActionResult> GetFilteredRecipes([FromQuery] List<string> categories, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = GetCurrentUserId();
            try
            {
                var (recipes, totalCount) = await _recipeReadingService.GetFilteredRecipes(userId, categories, pageNumber, pageSize);
                return Ok(new { recipes, totalCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
