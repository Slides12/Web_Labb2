using Microsoft.AspNetCore.Mvc;
using Web_Labb2.Api.Services;
using Web_Labb2.Shared.DTO_s;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO user)
        {
            var result = await _authService.ValidateUser(user);

            if(!result.IsValid)
                return Unauthorized();

            var token = _authService.GenerateJwtToken(user.Username, result.Role);
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            var user = await _authService.RegisterAsync(request);

            if (user is null)
                return BadRequest("Username already exists.");

            return Ok(user);
        }


    }
}
