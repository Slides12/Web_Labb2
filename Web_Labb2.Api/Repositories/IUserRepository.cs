using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        void UpdateUser(User user);
        void DeleteUser(User deleteUser);
    }
}
