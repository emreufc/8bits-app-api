using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _8bits_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteRecipesController : BaseController
    {
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
                var favorites = await _service.GetFavoritesByUserIdAsync(userId);

                return Ok(new
                {
                    code = 200,
                    message = "Favorites retrieved successfully.",
                    data = favorites
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

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("Unauthorized. User ID not found in token.");

            return int.Parse(userIdClaim.Value);
        }
    }

}
