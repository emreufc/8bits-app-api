using _8bits_app_api.Controllers;
using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ingredients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : BaseController
    {
        private readonly IIngredientReadingService _ingredientReadingService;

        public IngredientsController(IIngredientReadingService ingredientReadingService)
        {
            _ingredientReadingService = ingredientReadingService;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<IActionResult> GetIngredients([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
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

            var (ingredients, totalCount) = await _ingredientReadingService.GetIngredientsPaginatedAsync(pageNumber, pageSize);
            if (ingredients == null || !ingredients.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No ingredients found in the database. Please ensure that ingredients have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {ingredients.Count()} ingredients from the database.",
                data = ingredients,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                },
                
            });
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var ingredient = await _ingredientReadingService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No ingredient found with ID {id}. Please check the ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Ingredient with ID {id} retrieved successfully.",
                data = ingredient
            });
        }

        [HttpGet("by-category")]
        public async Task<IActionResult> GetByCategory(
        [FromQuery] List<string> categories,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            if (categories == null || categories.Count == 0)
                return BadRequest("Categories must be provided.");

            try
            {
                var result = await _ingredientReadingService.GetIngredientByCategoryAsync(categories, pageNumber, pageSize);
                return Ok(new
                {
                    Data = result.ingredients,
                    TotalCount = result.totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("admin-add-ingredient")]
        public async Task<IActionResult> Add([FromBody] Ingredient ingredient)
        {
            if(User.IsInRole("Admin")){
                await _ingredientReadingService.AddIngredientAsync(ingredient);
                var createdIngredient = await _ingredientReadingService.GetIngredientByIdAsync(ingredient.IngredientId);
                return CreatedAtAction(null, new { id = createdIngredient.Ingredient.IngredientId }, createdIngredient);  
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("admin-edit-ingredient")]
        public async Task<IActionResult> Edit([FromBody] Ingredient ingredient)
        {
            if (User.IsInRole("Admin"))
            {
                await _ingredientReadingService.UpdateIngredientAsync(ingredient);
                return Ok(new
                    {
                        code = 200,
                        message = $"Ingredient with ID {ingredient.IngredientId} updated successfully.",
                        data = ingredient
                    });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("admin-delete-ingredient")]
        public async Task<IActionResult> Delete([FromBody] int ingredientId)
        {
            if (User.IsInRole("Admin"))
            {
                await _ingredientReadingService.DeleteIngredientAsync(ingredientId);
                return Ok(new
                    {
                        code = 200,
                        message = $"Ingredient with ID {ingredientId} deleted successfully.",
                        data = ingredientId
                    });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
