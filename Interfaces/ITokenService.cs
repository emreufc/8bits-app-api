using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
    }
}
