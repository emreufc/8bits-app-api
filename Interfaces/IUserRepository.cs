using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(int userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<bool> UserExistsAsync(string email);
    Task<bool> UpdateUserAsync(User user);
}
