using Web_Labb2.Api.Repositories;
using Web_Labb2.Repositories;

namespace Web_Labb2.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IUserRepository Users { get; }


        Task<int> SaveChangesAsync();
    }
}
