using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeReadingService _recipeReadingService;

        public RecipesController(IRecipeReadingService recipeReadingService)
        {
            _recipeReadingService = recipeReadingService;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _recipeReadingService.GetAllRecipesAsync();
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
                data = recipes
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
    }
}
