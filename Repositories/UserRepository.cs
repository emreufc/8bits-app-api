using _8bits_app_api.Interfaces;
using _8bits_app_api.Models;
using Microsoft.EntityFrameworkCore;

namespace _8bits_app_api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly mydbcontext _context;

    public UserRepository(mydbcontext context)
    {
        _context = context;
    }
    
    public async Task<User> CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.Where(u => u.IsDeleted == false).ToListAsync(); 
    }
    
    public async Task<bool> UserExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email && u.IsDeleted == false);
    }
    
    public async Task<(IEnumerable<User> Users, int TotalCount)> SearchUsersAsync(string keyword, int pageNumber, int pageSize)
    {
        var query = _context.Users
            .Where(u => u.IsDeleted == false && 
                        (u.Name.Contains(keyword) || u.Surname.Contains(keyword) || u.Email.Contains(keyword)));

        var totalCount = await query.CountAsync();
        var users = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, totalCount);
    }

    public async Task<(IEnumerable<User> Users, int TotalCount)> GetAllUsersAsync(int pageNumber, int pageSize)
    {
        var query = _context.Users.Where(u => u.IsDeleted == false);

        var totalCount = await query.CountAsync();
        var users = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (users, totalCount);
    }
    
    public async Task<bool> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        return await _context.SaveChangesAsync() > 0;
    }
}
