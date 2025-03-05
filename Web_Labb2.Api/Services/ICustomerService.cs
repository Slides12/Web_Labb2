using Web_Labb2.DTO_s;

namespace Web_Labb2.Api.Services
{
    public interface ICustomerService
    {
        public  Task<IEnumerable<CustomerDTO>> GetAllCustomers();
        public  Task<CustomerDTO> GetCustomerByEmail(string email);
        public  Task<bool> AddCustomerAsync(CustomerDTO customer);
        public  Task<bool> DeleteCustomerByEmail(string email);
        public  Task<bool> UpdateCustomerByEmail(string email, CustomerDTO updatedCustomer);

    }
}
