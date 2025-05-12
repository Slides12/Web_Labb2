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


            return new AuthResult { IsValid = true, Role = user.Role, Email = user.Email };

        }


        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hasher = new PasswordHasher<User>();
            var verificationResult = hasher.VerifyHashedPassword(new User(), hashedPassword, password);

            return verificationResult == PasswordVerificationResult.Success;
        }



        public async Task<User?> RegisterAsync(UserDTO request)
        {
            var existingUser = await _unitOfWork.Users.GetUserByUsernameAsync(request.Username);
            if (existingUser != null)
                return null;


            var customer = new CustomerEntity
            {
                FirstName = "User",
                LastName = request.Username,
                Email = request.Email,
                PhoneNumber = null,
                AddressInformation = new AddressEntity()
            };

            await _unitOfWork.Customers.AddCustomerAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, request.Password),
                Role = request.Role,
                CustomerId = customer.Id
            };

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

        public async Task<bool> DeleteUsertByUsernam(string usernam)
        {
            var user = await _unitOfWork.Users.GetUserByUsernameAsync(usernam);
            if (user is null)
            {
                return false;
            }
            _unitOfWork.Users.DeleteUser(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(string username, UserDTO updatedUser)
        {
            if (updatedUser == null) return false;
            var existingUser = await _unitOfWork.Users.GetUserByUsernameAsync(username);
            if (existingUser == null) return false;

            existingUser.Username = string.IsNullOrEmpty(updatedUser.Username) ? updatedUser.Username : updatedUser.Username;
            existingUser.Role = string.IsNullOrEmpty(updatedUser.Role) ? updatedUser.Role : updatedUser.Role;
            existingUser.Email = string.IsNullOrEmpty(updatedUser.Email) ? updatedUser.Email : updatedUser.Email;


            _unitOfWork.Users.UpdateUser(existingUser);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<UserDTO> GetUserByUsername(string username)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByUsernameAsync(username);
                if (user != null)
                {
                    return new UserDTO()
                    {
                        Username = user.Username,
                        Role = user.Role,
                        Email = user.Email,
                        Password = user.PasswordHash,
                        CustomerId = user.CustomerId
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the product.", ex);
            }
        }
    }
}
