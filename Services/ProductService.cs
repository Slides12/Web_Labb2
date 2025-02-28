using Web_Labb2.Data;
using Web_Labb2.DTO_s;

namespace Web_Labb2.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return products.Select(p => new ProductDTO()
            { 
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductCategory = p.ProductCategory,
                Description = p.Description,
                Price = p.Price,
                Status = p.Status
            });
        }


        public async Task<bool> AddProductAsync(ProductDTO newProduct)
        {
            if (newProduct == null) 
                return false;

            await _unitOfWork.Products.AddProductAsync(new ProductEntity()
            {
                ProductId = newProduct.ProductId,
                ProductName = newProduct.ProductName,
                ProductCategory = newProduct.ProductCategory,
                Description = newProduct.Description,
                Price = newProduct.Price,
                Status = newProduct.Status
            });

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
