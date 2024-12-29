using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using _8bits_app_api.Dtos;
using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.IdentityModel.Tokens;

namespace _8bits_app_api.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IPasswordService _passwordService;

    public AuthService(IUserService userService, IConfiguration configuration, IPasswordService passwordService)
    {
        _userService = userService;
        _configuration = configuration;
        _passwordService = passwordService;
    }

    public async Task<string> RegisterAsync(RegisterModel model)
    {
        var dateOfBirth = DateOnly.FromDateTime(model.DateOfBirth);
        var user = new User
        {
            Name = model.Name,
            Surname = model.Surname,
            Email = model.Email,
            DateOfBirth = dateOfBirth,
            PhoneNumber = model.PhoneNumber,
            Role = "User"
        };

        var salt = _passwordService.GenerateSalt();
        user.PasswordSalt = salt;
        user.PasswordHash = _passwordService.HashPassword(model.Password, salt);

        await _userService.CreateUserAsync(user);

        return GenerateJwtToken(user); // Token oluşturma işlemi
    }

    public async Task<string> LoginAsync(LoginModel model)
    {
        var user = await _userService.GetUserByEmailAsync(model.Email);

        if (user == null || !_passwordService.ValidatePassword(model.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["JwtSettings:Issuer"],
            _configuration["JwtSettings:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
