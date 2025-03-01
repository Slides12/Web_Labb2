namespace Web_Labb2.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity> GetCustomerByEmailAsync(string email);
        Task AddCustomerAsync(CustomerEntity customer);
        void UpdateCustomer(CustomerEntity customer);
        void DeleteCustomer(CustomerEntity deleteCustomer);

    }
}
