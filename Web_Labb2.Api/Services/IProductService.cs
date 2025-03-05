using Web_Labb2.DTO_s;

namespace Web_Labb2.Api.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        public  Task<ProductDTO> GetProductById(string id);
        public  Task<ProductDTO> GetProductByName(string name);
        public  Task<bool> AddProductAsync(ProductDTO newProduct);
        public  Task<bool> UpdateProductAsync(string id, ProductDTO updatedProduct);
        public  Task<bool> UpdateProductStatus(string id);
        public  Task<bool> DeleteProductById(string id);

    }
}
