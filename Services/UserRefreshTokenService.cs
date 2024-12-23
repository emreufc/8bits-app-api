using _8bits_app_api.Models;
using _8bits_app_api.Repositories;
using System;
using System.Threading.Tasks;
using _8bits_app_api.Interfaces;

public class UserRefreshTokenService : IUserRefreshTokenService
{
    private readonly IUserRefreshTokenRepository _refreshTokenRepository;

    public UserRefreshTokenService(IUserRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<UserRefreshToken> CreateRefreshTokenAsync(int userId)
    {
        var refreshToken = new UserRefreshToken
        {
            RefreshToken = Guid.NewGuid().ToString(),
            UserId = userId,
            ExpirationDate = DateTime.UtcNow.AddDays(7)
        };

        return await _refreshTokenRepository.CreateAsync(refreshToken);
    }

    public async Task<UserRefreshToken> GetRefreshTokenAsync(string token)
    {
        return await _refreshTokenRepository.GetByTokenAsync(token);
    }
}