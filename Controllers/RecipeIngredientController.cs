using _8bits_app_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly mydbcontext _context;

        public RecipeIngredientController(mydbcontext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeIngredient>>> GetRecipeIngredient()
        {
            var recipeIngredients = await _context.RecipeIngredients.ToListAsync();
            if (recipeIngredients == null || recipeIngredients.Count == 0)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No Recipe ingredients found in the database. Please ensure that Recipe ingredients have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {recipeIngredients.Count()} Recipe ingredients from the database.",
                data = recipeIngredients
            });
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeIngredient>> GetRecipeIngredient(int id)
        {
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);

            if (recipeIngredient == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No Recipe ingredient found with ID {id}. Please check the ID and try again.",
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

        // GET: api/RecipeIngredients/recipe/{recipeId}
        [HttpGet("recipe/{recipeId}")]
        public async Task<ActionResult> GetRecipeIngredientsByRecipeId(int recipeId)
        {
            var recipeIngredients = await _context.RecipeIngredients
                                                   .Where(ri => ri.RecipeId == recipeId)
                                                   .OrderBy(ri => ri.IngredientId) // Opsiyonel
                                                   .ToListAsync();

            if (recipeIngredients == null || recipeIngredients.Count == 0)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No Recipe ingredients found with Recipe ID {recipeId}. Please check the Recipe ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {recipeIngredients.Count} ingredients for Recipe ID {recipeId}.",
                data = recipeIngredients
            });
        }

    }
}
