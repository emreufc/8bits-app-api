using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _8bits_app_api.Interfaces;

namespace _8bits_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminUserController : BaseController
    {
        private readonly IUserService _userService;

        public AdminUserController(IUserService userService)
        {
            _userService = userService;
        }

        // PUT: api/AdminUser/Update/{userId}
        [HttpPut("Update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User updatedUser)
        {
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

        // GET: api/AdminUser/GetUser/{userId}
        [HttpGet("GetUser/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "User not found.",
                    data = (object)null
                });
            }

            var userResponse = new
            {
                userId = user.UserId,
                name = user.Name,
                surname = user.Surname,
                email = user.Email,
                dateOfBirth = user.DateOfBirth,
                role = user.Role,
                gender = user.Gender,
                phoneNumber = user.PhoneNumber,
                imageUrl = user.ImageUrl
            };

            return Ok(new
            {
                code = 200,
                message = "User retrieved successfully.",
                data = userResponse
            });
        }

        // GET: api/AdminUser/keyword?keyword={keyword}&pageNumber={pageNumber}&pageSize={pageSize}
        [HttpGet("keyword")]
        public async Task<IActionResult> SearchUsers([FromQuery] string keyword, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var users = await _userService.SearchUsersAsync(keyword, pageNumber, pageSize);
            return Ok(new
            {
                code = 200,
                message = "Users retrieved successfully.",
                data = users.Users,
                totalItemCount = users.TotalCount
            });
        }

        // GET: api/AdminUser/GetAllUsers/{pageNumber}/{pageSize}
        [HttpGet("GetAllUsers/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAllUsers(int pageNumber, int pageSize)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);
            return Ok(new
            {
                code = 200,
                message = "All users retrieved successfully.",
                data = users.Users,
                totalItemCount = users.TotalCount
            });
        }
        
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            
            var result = await _userService.DeleteUserAsync(userId);
            if (!result)
            {
                return NotFound(new
                {
                    code = 404,
                    message = $"User with ID {userId} not found.",
                    data = (object)null
                });
            }

            return Ok(new
            {
                code = 200,
                message = $"User with ID {userId} has been successfully marked as deleted.",
                data = (object)null
            });
        }
    }
}
