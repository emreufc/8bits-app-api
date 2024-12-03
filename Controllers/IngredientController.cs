using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ingredients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientReadingService _IngredientReadingService;

        public IngredientsController(IIngredientReadingService ingredientReadingService)
        {
            _IngredientReadingService = ingredientReadingService;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<IActionResult> GetIngredients()
        {
            var Ingredients = await _IngredientReadingService.GetAllIngredientAsync();
            if (Ingredients == null || !Ingredients.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No Ingredients found in the database. Please ensure that Ingredients have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {Ingredients.Count()} Ingredients from the database.",
                data = Ingredients
            });
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var Ingredient = await _IngredientReadingService.GetIngredientByIdAsync(id);
            if (Ingredient == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No Ingredient found with ID {id}. Please check the ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Ingredient with ID {id} retrieved successfully.",
                data = Ingredient
            });
        }
    }
}
