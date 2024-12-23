using _8bits_app_api.Dtos;

namespace _8bits_app_api.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterModel model);
    Task<string> LoginAsync(LoginModel model);
}