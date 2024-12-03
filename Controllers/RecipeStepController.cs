using _8bits_app_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeStepController : ControllerBase
    {
        private readonly mydbcontext _context;

        public RecipeStepController(mydbcontext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeStep>>> GetRecipeStep()
        {
            var recipeSteps = await _context.RecipeSteps.ToListAsync();
            if (recipeSteps == null || recipeSteps.Count == 0)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No Recipe steps found in the database. Please ensure that Recipe steps have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {recipeSteps.Count()} Recipe steps from the database.",
                data = recipeSteps
            });
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeStep>> GetRecipeStep(int id)
        {
            var recipeStep = await _context.RecipeSteps.FindAsync(id);

            if (recipeStep == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No Recipe step found with ID {id}. Please check the ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Recipe step with ID {id} retrieved successfully.",
                data = recipeStep
            });
        }

        // GET: api/Recipes/steps/recipe/{recipeId}
        [HttpGet("steps/recipe/{recipeId}")]
        public async Task<ActionResult> GetRecipeStepsByRecipeId(int recipeId)
        {
            var recipeSteps = await _context.RecipeSteps
                                            .Where(rs => rs.RecipeId == recipeId)
                                            .ToListAsync();

            if (recipeSteps == null || recipeSteps.Count == 0)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No Recipe steps found with Recipe ID {recipeId}. Please check the Recipe ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {recipeSteps.Count} steps for Recipe ID {recipeId}.",
                data = recipeSteps
            });
        }

    }
}
