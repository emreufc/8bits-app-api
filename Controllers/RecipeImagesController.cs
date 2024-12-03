using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace RecipeImages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesImagesController : ControllerBase
    {
        private readonly IRecipeImagesReadingService _recipeimageReadingService;

        public RecipesImagesController(IRecipeImagesReadingService recipeImagesReadingService)
        {
            _recipeimageReadingService = recipeImagesReadingService;
        }

        // GET: api/RecipesImages
        [HttpGet]
        public async Task<IActionResult> GetRecipeImages()
        {
            var recipes = await _recipeimageReadingService.GetAllRecipeImagesAsync();
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
        public async Task<IActionResult> GetRecipeImage(int id)
        {
            var recipe = await _recipeimageReadingService.GetRecipeImageById(id);
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
