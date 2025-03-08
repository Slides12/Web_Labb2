using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Web_Labb2.Api.Controllers
{
    [Authorize] // Protects all endpoints in this controller
    [Route("api/[controller]")]
    [ApiController]
    public class SecureController : ControllerBase
    {
        [HttpGet("secure-endpoint")]
        public IActionResult GetSecureData()
        {
            var identity = HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            if (identity != null)
            {
                var claims = identity.Claims.Select(c => new { c.Type, c.Value });
                return Ok(new { Message = "You are authorized!", Claims = claims });
            }
            return Unauthorized();
        }
    }
}
