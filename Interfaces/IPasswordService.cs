namespace _8bits_app_api.Interfaces;

public interface IPasswordService
{
    string GenerateSalt();
    string HashPassword(string password, string salt);
    bool ValidatePassword(string hashedPassword, string salt, string password);
}