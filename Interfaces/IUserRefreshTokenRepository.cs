
using System.Threading.Tasks;
using _8bits_app_api.Models;
using System;

namespace _8bits_app_api.Interfaces;

public interface IUserRefreshTokenRepository
{
    Task<UserRefreshToken> GetByTokenAsync(string token);
    Task<UserRefreshToken> CreateAsync(UserRefreshToken refreshToken);
}
