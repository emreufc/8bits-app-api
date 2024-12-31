using _8bits_app_api.Models;
using _8bits_app_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;

namespace _8bits_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IUserRefreshTokenService _refreshTokenService;

        public AuthController(IUserService userService, IPasswordService passwordService, ITokenService tokenService, IUserRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }

        // Register Endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // Check if the user already exists
            var existingUser = await _userService.GetUserByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email already exists." });
            }

            // Generate salt and hash password
            var salt = _passwordService.GenerateSalt();
            var hashedPassword = _passwordService.HashPassword(model.Password, salt);
            var dateOfBirth = DateOnly.FromDateTime(model.DateOfBirth);

            // Create user entity
            var user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Gender = model.Gender,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                DateOfBirth = dateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Role = "User",
                IsDeleted = false
            };

            // Save user to database
            var createdUser = await _userService.CreateUserAsync(user);

            // Generate tokens
            var token = _tokenService.GenerateJwtToken(createdUser);
            var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(createdUser.UserId);

            return Ok(new
            {
                message = "Login successful.",
                accessToken = token,
                uid = createdUser.UserId,
                expiration = DateTime.UtcNow.AddMinutes(30),
                refreshToken = refreshToken.RefreshToken
            });
        }


        // Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.GetUserByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "User not found for this email" });
            }

            var isValidPassword = _passwordService.ValidatePassword(user.PasswordHash, user.PasswordSalt, model.Password);
            if (!isValidPassword)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = _tokenService.GenerateJwtToken(user);
            var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user.UserId);

            return Ok(new
            {
                message = "Login successful.",
                accessToken = token,
                uid = user.UserId,
                expiration = DateTime.UtcNow.AddMinutes(30),
                refreshToken = refreshToken.RefreshToken
            });
        }

        // Refresh Token Endpoint
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest model)
        {
            var refreshToken = await _refreshTokenService.GetRefreshTokenAsync(model.RefreshToken);
            if (refreshToken == null || refreshToken.ExpirationDate < DateTime.UtcNow)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            }

            var user = await _userService.GetUserByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            
            var newToken = _tokenService.GenerateJwtToken(user);
            var newRefreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user.UserId);

            return Ok(new
            {
                message = "Token refreshed successfully.",
                token = newToken,
                refreshToken = newRefreshToken.RefreshToken
            });
        }
    }
}
