using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Web_Labb2.Api.Services;
using Web_Labb2.Data;
using Web_Labb2.DTO_s;


namespace Web_Labb2.Services
{
    public class CustomerService : ICustomerService
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
                        AddressInformation = new AddressEntity()
                        {
                            Address = customer.AddressInformation.Address,
                            PostNumber = customer.AddressInformation.PostNumber,
                            City = customer.AddressInformation.City,
                            Country = customer.AddressInformation.Country,
                        },
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
            _unitOfWork.Customers.DeleteCustomer(customer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateCustomerByEmail(string email, CustomerDTO updatedCustomer)
        {
            if (string.IsNullOrEmpty(email) || updatedCustomer == null)
            {
                return false;
            }

            var existingCustomer = await _unitOfWork.Customers.GetCustomerByEmailAsync(email);

            if (existingCustomer == null)
            {
                return false;
            }

                existingCustomer.FirstName = string.IsNullOrEmpty(updatedCustomer.FirstName) ? existingCustomer.FirstName : updatedCustomer.FirstName;
                existingCustomer.LastName = string.IsNullOrEmpty(updatedCustomer.LastName) ? existingCustomer.LastName : updatedCustomer.LastName;
                existingCustomer.PhoneNumber = string.IsNullOrEmpty(updatedCustomer.PhoneNumber) ? existingCustomer.PhoneNumber : updatedCustomer.PhoneNumber;



            if (updatedCustomer.AddressInformation != null)
            {
                existingCustomer.AddressInformation.Address = string.IsNullOrEmpty(updatedCustomer.AddressInformation.Address) ? existingCustomer.AddressInformation.Address : updatedCustomer.AddressInformation.Address;
                existingCustomer.AddressInformation.PostNumber = string.IsNullOrEmpty(updatedCustomer.AddressInformation.PostNumber) ? existingCustomer.AddressInformation.PostNumber : updatedCustomer.AddressInformation.PostNumber;
                existingCustomer.AddressInformation.City = string.IsNullOrEmpty(updatedCustomer.AddressInformation.City) ? existingCustomer.AddressInformation.City : updatedCustomer.AddressInformation.City;
                existingCustomer.AddressInformation.Country = string.IsNullOrEmpty(updatedCustomer.AddressInformation.Country) ? existingCustomer.AddressInformation.Country : updatedCustomer.AddressInformation.Country;
            }

                _unitOfWork.Customers.UpdateCustomer(existingCustomer);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


    }
}
