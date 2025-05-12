
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

        public void UpdateCustomer(CustomerEntity customer)
        {
            _context.CustomerEntitys.Update(customer);
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.CustomerEntitys.Include(c => c.AddressInformation).ToListAsync();
        }

        public async Task<CustomerEntity> GetCustomerByEmailAsync(string email)
        {
            return await _context.CustomerEntitys.Include(c => c.AddressInformation).FirstOrDefaultAsync(c => c.Email == email);
        }

        public void DeleteCustomer(CustomerEntity deleteCustomer)
        {
            _context.CustomerEntitys.Remove(deleteCustomer);
        }
    }

}
