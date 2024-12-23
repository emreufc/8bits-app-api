using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using _8bits_app_api.Interfaces;

namespace _8bits_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // PUT: api/Users/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User updatedUser)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new
                {
                    code = 401,
                    message = "Unauthorized. User ID not found in token.",
                    data = (object)null
                });
            }

            var userId = int.Parse(userIdClaim.Value);

            var result = await _userService.UpdateUserAsync(userId, updatedUser);
            if (!result)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Failed to update user. Please check your input and try again.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"User with ID {userId} updated successfully.",
                data = updatedUser
            });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new
                {
                    code = 401,
                    message = "Unauthorized. User ID not found in token.",
                    data = (object)null
                });
            }

            var currentUserId = int.Parse(userIdClaim.Value);

            var result = await _userService.DeleteUserAsync(currentUserId);
            if (!result)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"User with ID {currentUserId} not found.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"User with ID {currentUserId} has been successfully marked as deleted.",
                data = (object)null
            });
        }


        // GET: api/Users/current
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new
                {
                    code = 401,
                    message = "Unauthorized. User ID not found in token.",
                    data = (object)null
                });
            }

            var userId = int.Parse(userIdClaim.Value);

            var user = await _userService.GetUserByIdAsync(userId);

            var userResponse = new
            {
                userId = user.UserId,
                name = user.Name,
                email = user.Email,
                dateOfBirth = user.DateOfBirth,
                role = user.Role
            };
            if (user == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "User not found.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = "User retrieved successfully.",
                data = userResponse
            });
        }
    }
}
