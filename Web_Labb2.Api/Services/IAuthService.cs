using Web_Labb2.Data;
using Web_Labb2.DTO_s;
using Web_Labb2.Shared.DTO_s;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Services
{
    public interface IAuthService
    {
        public string GenerateJwtToken(string username, string role);
        public Task<AuthResult> ValidateUser(LoginDTO request);
        public Task<User?> RegisterAsync(UserDTO request);
        public Task<IEnumerable<UserDTO>> GetAllUsers();
        public Task<bool> DeleteUsertByUsernam(string usernam);
        public Task<bool> UpdateUserAsync(string username, UserDTO updatedUser);
        public Task<UserDTO> GetUserByUsername(string username);


    }
}
