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
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                AddressInformation = c.AddressInformation,
                PhoneNumber = c.PhoneNumber,
            });
        }

    }
}
