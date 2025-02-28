
using Microsoft.EntityFrameworkCore;

namespace Web_Labb2.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly APIDBContext _context;

        public ProductRepository(APIDBContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(ProductEntity newProduct)
        {
            await _context.ProductEntitys.AddAsync(newProduct);
        }

        public void DeleteProduct(ProductEntity deleteProduct)
        {
            _context.ProductEntitys.Remove(deleteProduct);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _context.ProductEntitys.ToListAsync();
        }

        public async Task<ProductEntity> GetProductByProductNameAsync(string productName)
        {
            return await _context.ProductEntitys.FirstOrDefaultAsync(p => p.ProductName == productName);
        }

        public async Task<ProductEntity> GetProductByProductNumberAsync(string productId)
        {
            return await _context.ProductEntitys.FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public void UpdateProduct(ProductEntity updateProduct)
        {
            _context.ProductEntitys.Update(updateProduct);
        }

       
    }
}
