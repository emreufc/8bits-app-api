using _8bits_app_api.Controllers;
using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace _8_bits.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeStepController : BaseController
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

        [HttpPost("admin-add-recipe-step")]
        public async Task<IActionResult> AddSteps(int recipeId, [FromBody] List<RecipeStep> steps)
        {
            if (User.IsInRole("Admin"))
            {
                if (steps == null || !steps.Any())
                {
                    return BadRequest(new { message = "Step list cannot be empty." });
                }

                await _recipeStepService.AddRecipeStepsAsync(recipeId, steps);
                return Ok(new { message = "Recipe steps added successfully." });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("admin-update-recipe-step")]
        public async Task<IActionResult> UpdateSteps(int recipeId, [FromBody] RecipeStep step)
        {
            if (User.IsInRole("Admin"))
            {
                if (step == null)
                {
                    return BadRequest(new { message = "Invalid data." });
                }

                // Ensure the provided RecipeStepsId matches the step's ID
                step.RecipeId = recipeId;

                try
                {
                    await _recipeStepService.UpdateRecipeStepAsync(recipeId, step);
                    return Ok(new { message = "Recipe step updated successfully." });
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new { message = ex.Message });
                }
                
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("admin-delete-recipe-step")]
        public async Task<IActionResult> DeleteStep(int recipeId, byte stepNum)
        {
            if (User.IsInRole("Admin"))
            {
                try
                {
                    await _recipeStepService.DeleteRecipeStepAsync(recipeId, stepNum);
                    return Ok(new {code=200, message = "Recipe step deleted successfully." });
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new { message = ex.Message });
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
