using Microsoft.AspNetCore.Mvc;

namespace _8bits_app_api.Controllers;

public class TestController : Controller
{
    // GET
    public IActionResult Index()
    {
        return Ok("Hayirli ugurlu olsun");
    }
}