using PriceNegotiationAPI.Domain.Entities;

namespace PriceNegotiationAPI.Domain.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken ct = default);
    Task<Product> GetProductByIdAsync(int productId, CancellationToken ct = default);
    Task AddProductAsync(Product product, CancellationToken ct = default);
    Task DeleteProductAsync(int productId, CancellationToken ct = default);
}