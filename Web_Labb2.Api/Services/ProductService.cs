using Web_Labb2.Api.Services;
using Web_Labb2.Data;
using Web_Labb2.DTO_s;

namespace Web_Labb2.Services
{
    public class ProductService : IProductService
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
                Status = p.Status,
                ImagePath = p.ImagePath
            });
        }

        public async Task<ProductDTO> GetProductById(string id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetProductByProductNumberAsync(id);
                if(product != null)
                {
                    return new ProductDTO() 
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductCategory = product.ProductCategory,
                        Description = product.Description,
                        Price = product.Price,
                        Status = product.Status,
                        ImagePath = product.ImagePath
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the product.", ex);
            }
        }

        public async Task<ProductDTO> GetProductByName(string name)
        {
            try
            {
                var product = await _unitOfWork.Products.GetProductByProductNameAsync(name);
                if (product != null)
                {
                    return new ProductDTO()
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductCategory = product.ProductCategory,
                        Description = product.Description,
                        Price = product.Price,
                        Status = product.Status,
                        ImagePath = product.ImagePath
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving the product.", ex);
            }
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
                Status = newProduct.Status,
                ImagePath = newProduct.ImagePath
            });

            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateProductAsync(string id, ProductDTO updatedProduct)
        {
            if (updatedProduct == null) return false;
            var existingProduct = await _unitOfWork.Products.GetProductByProductNumberAsync(id);
            if (existingProduct == null) return false;

            existingProduct.ProductName = string.IsNullOrEmpty(updatedProduct.ProductName) ? existingProduct.ProductName : updatedProduct.ProductName;
            existingProduct.Description = string.IsNullOrEmpty(updatedProduct.Description) ? existingProduct.Description : updatedProduct.Description;
            existingProduct.ProductCategory = string.IsNullOrEmpty(updatedProduct.ProductCategory) ? existingProduct.ProductCategory : updatedProduct.ProductCategory;
            existingProduct.ImagePath = string.IsNullOrEmpty(updatedProduct.ImagePath) ? existingProduct.ImagePath : updatedProduct.ImagePath;

            if (updatedProduct.Price > 1)
            {
                existingProduct.Price = updatedProduct.Price;
            }


            _unitOfWork.Products.UpdateProduct(existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateProductStatus(string id)
        {
            var existingProduct = await _unitOfWork.Products.GetProductByProductNumberAsync(id);
            if (existingProduct == null) return false;
            existingProduct.Status = !existingProduct.Status;
            _unitOfWork.Products.UpdateProduct(existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductById(string id)
        {

            var product = await _unitOfWork.Products.GetProductByProductNumberAsync(id);
            if (product is null)
            {
                return false;
            }
            _unitOfWork.Products.DeleteProduct(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
