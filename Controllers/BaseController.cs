using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _8bits_app_api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Unauthorized. User ID not found in token.");
            }

            return int.Parse(userIdClaim.Value);
        }
    }

}
