
using Microsoft.EntityFrameworkCore;
using Web_Labb2.Data;

namespace Web_Labb2.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly APIDBContext _context;

        public CustomerRepository(APIDBContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(CustomerEntity customer)
        {
            await _context.CustomerEntitys.AddAsync(customer);
        }

        public void Update(CustomerEntity customer)
        {
            _context.CustomerEntitys.Update(customer);
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.CustomerEntitys.ToListAsync();
        }

        public async Task<CustomerEntity> GetCustomerByEmailAsync(string email)
        {
            return await _context.CustomerEntitys.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task DeleteCustomerAsync(CustomerEntity deleteCustomer)
        {
            _context.CustomerEntitys.Remove(deleteCustomer);
        }
    }

}
