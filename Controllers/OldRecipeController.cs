using _8bits_app_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _8bits_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldRecipeController : BaseController
    {
        private readonly IOldRecipesService _oldRecipeService;

        public OldRecipeController(IOldRecipesService oldRecipeService)
        {
            _oldRecipeService = oldRecipeService;
        }
        [HttpPost("OldRecipe-ADD")]
        public async Task<IActionResult> MarkRecipeAsDone([FromBody] int recipeId)
        {

            var userId = GetCurrentUserId();

            try
            {
                await _oldRecipeService.AddRecipeToOldRecipesAsync(recipeId, userId);
                return Ok(new
                {
                    code = 200, 
                    message = "Recipe successfully moved to old recipes.",
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }
        [HttpPost("ToggleOldRecipe")]
        public async Task<IActionResult> ToggleOldRecipe(int recipeId)
        {
            var userId = GetCurrentUserId();

            try
            {
                // Tarifin old recipes listesinde olup olmadığını kontrol et
                var isOldRecipe = await _oldRecipeService.IsUserOldRecipeAsync(userId, recipeId);

                if (isOldRecipe)
                {
                    // Tarif eski tariflerdeyse sil
                    var result = await _oldRecipeService.DeleteRecipeFromOldRecipesAsync(recipeId, userId);

                    return Ok(new
                    {
                        code = 200,
                        message = "Recipe successfully removed from old recipes.",
                        data = new { isOldRecipe = false }
                    });
                }
                else
                {
                    // Tarif eski tariflerde değilse ekle
                    await _oldRecipeService.AddRecipeToOldRecipesAsync(recipeId, userId);

                    return Ok(new
                    {
                        code = 200,
                        message = "Recipe successfully added to old recipes.",
                        data = new { isOldRecipe = true }
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }

        [HttpGet("OldRecipe-GET")]
        public async Task<IActionResult> GetMyOldRecipes()
        {

            var userId = GetCurrentUserId();

            try
            {
                var oldRecipes = await _oldRecipeService.GetOldRecipesByUserIdAsync(userId);

                if (oldRecipes == null || !oldRecipes.Any())
                {
                    return NotFound(new
                    {
                        code = 404,
                        message = "No old recipes found for the current user.",
                        data = (object)null
                    });
                }

                return Ok(new
                {
                    code = 200,
                    message = "Old recipes retrieved successfully.",
                    data = oldRecipes
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = ex.Message,
                    data = (object)null
                });
            }
        }
    }
}
