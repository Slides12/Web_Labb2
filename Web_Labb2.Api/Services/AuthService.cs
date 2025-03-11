using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Web_Labb2.Data;
using Web_Labb2.DTO_s;
using Web_Labb2.Shared.DTO_s;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _jwtSecret;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            var options = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            _jwtSecret = options["jwtKey"];
            _issuer = config["JwtSettings:Issuer"];
            _audience = config["JwtSettings:Audience"];
        }

        public string GenerateJwtToken(string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<AuthResult> ValidateUser(LoginDTO request)
        {

            var user = await _unitOfWork.Users.GetUserByUsernameAsync(request.Username);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            {
                return new AuthResult { IsValid = false};
            }


            return new AuthResult { IsValid = true, Role = user.Role };

        }


        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hasher = new PasswordHasher<User>();
            var verificationResult = hasher.VerifyHashedPassword(new User(), hashedPassword, password);

            return verificationResult == PasswordVerificationResult.Success;
        }



        public async Task<User?> RegisterAsync(UserDTO request)
        {
            if (await _unitOfWork.Users.GetUserByUsernameAsync(request.Username) != null)
            {
                return null;
            }

            var user = new User();

            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PasswordHash = hashedPassword;
            user.Role = request.Role;

            await _unitOfWork.Users.AddUserAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var customers = await _unitOfWork.Users.GetAllAsync();

            return customers.Select(u => new UserDTO
            {
               Username = u.Username,
               Role = u.Role,
               Email = u.Email,
               Password = u.PasswordHash
            });
        }
    }
}
