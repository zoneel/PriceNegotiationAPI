using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IConfiguration _configuration;

    public ProductRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    private IDbConnection Connection => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        using IDbConnection conn = Connection;
        conn.Open();
        var products = await conn.QueryAsync<Product>("SELECT * FROM Products");
        return products;
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        using IDbConnection conn = Connection;
        conn.Open();
        var product = await conn.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Products WHERE ProductID = @ProductId", new { ProductId = productId });
        return product;
    }

    public async Task AddProductAsync(Product product)
    {
        using IDbConnection conn = Connection;
        conn.Open();
        await conn.ExecuteAsync("INSERT INTO Products (ProductID, Name, BasePrice) VALUES (@ProductID, @Name, @BasePrice)", product);
    }

    public async Task DeleteProductAsync(int productId)
    {
        using IDbConnection conn = Connection;
        conn.Open();
        await conn.ExecuteAsync("DELETE FROM Products WHERE ProductID = @ProductId", new { ProductId = productId });
    }
}