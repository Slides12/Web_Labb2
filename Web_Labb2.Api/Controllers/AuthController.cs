using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Labb2.Api.Services;
using Web_Labb2.DTO_s;
using Web_Labb2.Services;
using Web_Labb2.Shared.DTO_s;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: api/<CustomerController>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllUsers()
        {
            var users = await _authService.GetAllUsers();
            return Ok(users);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO user)
        {
            var result = await _authService.ValidateUser(user);

            if(!result.IsValid)
                return Unauthorized();

            var token = _authService.GenerateJwtToken(user.Username, result.Role);
            result.Token = token;
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


        [Authorize(Roles = "Admin")]
        [HttpGet("get-user/{username}")]
        public async Task<ActionResult<ProductDTO>> GetUserByName(string username)
        {
            var result = await _authService.GetUserByUsername(username);
            if (result == null)
            {
                return NotFound("There is no user by this name.");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{username}")]
        public async Task<ActionResult> PutAllInfo(string username, [FromBody] UpdateUserDTO updatedUser)
        {
            Console.WriteLine($"Received PUT request for product ID: {updatedUser.Username}");

            if (string.IsNullOrEmpty(username)) return BadRequest("You need to enter a username.");
            if (updatedUser == null) return BadRequest("You need to add data to be updated.");

            var updateUserDTO = new UserDTO
            {
                Username = updatedUser.Username,
                Role = updatedUser.Role,
                Email = updatedUser.Email,
            };

            var result = await _authService.UpdateUserAsync(username, updateUserDTO);
            if (!result)
                return NotFound("Did not find a user by that name.");

            return Ok("User was updated successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteProduct(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("You need to input a correct username.");
            }

            var result = await _authService.DeleteUsertByUsernam(username);
            if (!result)
            {
                return NotFound("No user with that Name exists.");
            }

            return NoContent();
        }


    }
}
