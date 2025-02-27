using Web_Labb2.Repositories;

namespace Web_Labb2.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APIDBContext _context;

        public ICustomerRepository Customers { get; private set; }
        public IProductRepository Products { get; private set; }
        public IAdressRepository Addresses { get; private set; }

        public UnitOfWork(APIDBContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            //Products = new ProductRepository(_context);
            //Addresses = new AdressRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
