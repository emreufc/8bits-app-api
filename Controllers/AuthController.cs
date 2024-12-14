using Microsoft.AspNetCore.Mvc;

namespace _8bits_app_api.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCurrentUser()
    {
        string user = "Emre";
        return Ok(user);
    }
}