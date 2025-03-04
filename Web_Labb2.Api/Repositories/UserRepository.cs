using Microsoft.EntityFrameworkCore;
using Web_Labb2.Shared.Models;

namespace Web_Labb2.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly APIDBContext _context;

        public UserRepository(APIDBContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.UserEntitys.AddAsync(user);
        }

        public void DeleteUser(User deleteUser)
        {
             _context.UserEntitys.Remove(deleteUser);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.UserEntitys.ToListAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.UserEntitys.FirstOrDefaultAsync(u => u.Username == username);
        }

        public void UpdateUser(User user)
        {
            _context.UserEntitys.Update(user);
        }
    }
}
