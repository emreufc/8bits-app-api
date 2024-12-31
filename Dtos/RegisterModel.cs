namespace _8bits_app_api.Dtos;

public class RegisterModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public byte Gender { get; set; }
    
}