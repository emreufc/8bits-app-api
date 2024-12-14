using Microsoft.AspNetCore.Mvc;

namespace _8bits_app_api.Interfaces;

public interface IUsersService
{
    public IActionResult GetCurrentUser();
}