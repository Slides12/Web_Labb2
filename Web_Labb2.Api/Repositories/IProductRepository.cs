namespace Web_Labb2.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task<ProductEntity> GetProductByProductNumberAsync(string productId);
        Task<ProductEntity> GetProductByProductNameAsync(string productName);
        Task AddProductAsync(ProductEntity newProduct);
        void UpdateProduct(ProductEntity updateProduct);
        void DeleteProduct(ProductEntity deleteProduct);
    }
}
