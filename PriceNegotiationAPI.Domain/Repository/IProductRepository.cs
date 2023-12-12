using PriceNegotiationAPI.Domain.Entities;

namespace PriceNegotiationAPI.Domain.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int productId);
    Task AddProductAsync(Product product);
    Task DeleteProductAsync(int productId);
}