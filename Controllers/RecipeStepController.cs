using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeStepController : ControllerBase
    {
        private readonly IRecipeStepReadingService _recipeStepService;

        public RecipeStepController(IRecipeStepReadingService recipeStepService)
        {
            _recipeStepService = recipeStepService;
        }

        // GET: api/RecipeStep
        [HttpGet]
        public async Task<IActionResult> GetRecipeSteps([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
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

            var (recipeSteps, totalCount) = await _recipeStepService.GetRecipeStepsPaginatedAsync(pageNumber, pageSize);
            if (recipeSteps == null || !recipeSteps.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = "No recipe steps found in the database. Please ensure that recipe steps have been added before querying.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {recipeSteps.Count()} recipe steps from the database.",
                data = recipeSteps,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize,
                    totalRecords = totalCount,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                }
            });
        }

        // GET: api/RecipeStep/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeStep(int id)
        {
            var recipeStep = await _recipeStepService.GetRecipeStepByIdAsync(id);
            if (recipeStep == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No recipe step found with ID {id}. Please check the ID and try again.",
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

        // GET: api/RecipeStep/steps/recipe/{recipeId}
        [HttpGet("steps/recipe/{recipeId}")]
        public async Task<IActionResult> GetRecipeStepsByRecipeId(int recipeId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
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

            var (recipeSteps, totalCount) = await _recipeStepService.GetRecipeStepsByRecipeIdPaginatedAsync(recipeId, pageNumber, pageSize);
            if (recipeSteps == null || !recipeSteps.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"No recipe steps found with Recipe ID {recipeId}. Please check the Recipe ID and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"Successfully retrieved {recipeSteps.Count()} steps for Recipe ID {recipeId}.",
                data = recipeSteps,
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
