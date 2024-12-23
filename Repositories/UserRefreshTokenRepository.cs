using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using _8bits_app_api.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace _8bits_app_api.Repositories;
public class UserRefreshTokenRepository : IUserRefreshTokenRepository
{
    private readonly mydbcontext _context;

    public UserRefreshTokenRepository(mydbcontext context)
    {
        _context = context;
    }

    public async Task<UserRefreshToken> GetByTokenAsync(string token)
    {
        return await _context.UserRefreshTokens.FirstOrDefaultAsync(rt => rt.RefreshToken == token);
    }

    public async Task<UserRefreshToken> CreateAsync(UserRefreshToken refreshToken)
    {
        await _context.UserRefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
        return refreshToken;
    }
}
