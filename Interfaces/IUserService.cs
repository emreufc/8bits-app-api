using _8bits_app_api.Models;

namespace _8bits_app_api.Interfaces;

public interface IUserService
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task<User> GetUserByIdAsync(int userId);
    Task<bool> UpdateUserAsync(int userId, User updatedUser);
    Task<bool> DeleteUserAsync(int userId);
    
}
