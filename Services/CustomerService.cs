using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Web_Labb2.Data;
using Web_Labb2.DTO_s;


namespace Web_Labb2.Services
{
    public class CustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<CustomerDTO>> GetAllCustomers()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();

            return customers.Select(c => new CustomerDTO
            { 
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                AddressInformation = c.AddressInformation,
                PhoneNumber = c.PhoneNumber,
            });
        }

        public async Task<CustomerDTO> GetCustomerByEmail(string email)
        {

            var customer = await _unitOfWork.Customers.GetCustomerByEmailAsync(email);

            if(customer != null)
            {


                var customerDTO = new CustomerDTO() {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    AddressInformation = customer.AddressInformation,
                    PhoneNumber = customer.PhoneNumber,

                };

                return customerDTO;
            }
            return null;
        }

        public async Task<bool> AddCustomerAsync(CustomerDTO customer)
        {
            if(!string.IsNullOrEmpty(customer.FirstName) || !string.IsNullOrEmpty(customer.Email))
            {
                var customerEmail = await _unitOfWork.Customers.GetCustomerByEmailAsync(customer.Email);

                if(customerEmail is null) 
                { 
                    await _unitOfWork.Customers.AddCustomerAsync(new CustomerEntity() { 
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        AddressInformation = new AddressEntity(),
                        PhoneNumber = customer.PhoneNumber,
                    });
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteCustomerByEmail(string email)
        {

            var customer = await _unitOfWork.Customers.GetCustomerByEmailAsync(email);
            if(customer is null)
            {
                return false;
            }
            await _unitOfWork.Customers.DeleteCustomerAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
