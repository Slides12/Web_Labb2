using Web_Labb2.Data;
using Web_Labb2.Shared.DTO_s;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Services
{
    public interface IAuthService
    {
        public string GenerateJwtToken(string username, string role);
        public Task<AuthResult> ValidateUser(LoginDTO request);
        public Task<User?> RegisterAsync(UserDTO request);
    }
}
