using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces;

public interface IUserRefreshTokenService
{
    Task<UserRefreshToken> CreateRefreshTokenAsync(int userId);
    Task<UserRefreshToken> GetRefreshTokenAsync(string token);
}