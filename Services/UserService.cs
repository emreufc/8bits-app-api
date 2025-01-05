using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        return await _userRepository.CreateUserAsync(user);
    }
    
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }

    public async Task<bool> UpdateUserAsync(int userId, User updatedUser)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(userId);
        if (existingUser == null)
        {
            return false;
        }

        existingUser.Name = updatedUser.Name ?? existingUser.Name;
        existingUser.Email = updatedUser.Email ?? existingUser.Email;
        existingUser.Surname = updatedUser.Surname ?? existingUser.Surname;
        existingUser.Role = updatedUser.Role ?? existingUser.Role;
        existingUser.PhoneNumber = updatedUser.PhoneNumber ?? existingUser.PhoneNumber;
        existingUser.DateOfBirth = updatedUser.DateOfBirth ?? existingUser.DateOfBirth;
        existingUser.Gender = updatedUser.Gender ?? existingUser.Gender;
        // Update other properties as needed...

        return await _userRepository.UpdateUserAsync(existingUser);
    }
    
    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.IsDeleted = true;
        return await _userRepository.UpdateUserAsync(user);
    }
    
    public async Task<(IEnumerable<User> Users, int TotalCount)> SearchUsersAsync(string keyword, int pageNumber, int pageSize)
    {
        return await _userRepository.SearchUsersAsync(keyword, pageNumber, pageSize);
    }

    public async Task<(IEnumerable<User> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize)
    {
        return await _userRepository.GetAllUsersAsync(pageNumber, pageSize);
    }
}
