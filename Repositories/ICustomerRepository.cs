namespace Web_Labb2.Repositories
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(CustomerEntity customer);
        void Update(CustomerEntity customer);
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<CustomerEntity> GetCustomerByEmailAsync(string email);
        Task DeleteCustomerAsync(CustomerEntity deleteCustomer);

    }
}
