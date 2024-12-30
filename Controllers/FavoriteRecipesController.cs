using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _8bits_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteRecipesController : BaseController
    {
        private readonly IRecipeReadingService _recipeservice;
        private readonly IFavoriteRecipeService _service;

        public FavoriteRecipesController(IFavoriteRecipeService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRecipeDto dto)
        {
            try
            {
                dto.UserId = GetCurrentUserId();
                var result = await _service.AddFavoriteAsync(dto);
                return Ok(new
                {
                    code = 200,
                    message = "Favorite added successfully.",
                    data = result
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    code = 401,
                    message = ex.Message,
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

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFavorite(int recipeId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var success = await _service.RemoveFavoriteAsync(userId, recipeId);

                if (success)
                {
                    return Ok(new
                    {
                        code = 200,
                        message = "Favorite removed successfully.",
                        data = (object)null
                    });
                }

                return NotFound(new
                {
                    code = 404,
                    message = "Favorite not found.",
                    data = (object)null
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    code = 401,
                    message = ex.Message,
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

        [HttpGet("user-favorites")]
        public async Task<IActionResult> GetFavoritesForCurrentUser()
        {
            try
            {
                var userId = GetCurrentUserId();
                var favoriteRecipes = await _service.GetFavoritesByUserIdAsync(userId);

                if (favoriteRecipes == null || !favoriteRecipes.Any())
                {
                    return NotFound(new
                    {
                        code = 404,
                        message = "No favorite recipes found.",
                        data = (object)null
                    });
                }
                return Ok(new
                {
                    code = 200,
                    message = "Favorites retrieved successfully.",
                    data = favoriteRecipes
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    code = 401,
                    message = ex.Message,
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
    }

}
