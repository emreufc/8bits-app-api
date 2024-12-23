using _8bits_app_api.Interfaces;
using BCrypt.Net;

namespace _8bits_app_api.Services;

public class PasswordService : IPasswordService
{
    public string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt();
    }

    public string HashPassword(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool ValidatePassword(string hashedPassword, string salt, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
