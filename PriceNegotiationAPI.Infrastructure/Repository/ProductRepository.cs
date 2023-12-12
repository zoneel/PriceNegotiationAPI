using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PriceNegotiationAPI.Application.Product.Dto;
using PriceNegotiationAPI.Application.Product.Mapping;
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

    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken ct = default)
    {
        using IDbConnection conn = Connection;
        conn.Open();
        var productsDto = await conn.QueryAsync<ProductDto>("SELECT * FROM Products");

        var products = new List<Product>();
        foreach (var dto in productsDto)
        {
            var product = ProductMapping.MapProductDtoToProductEntity(dto);
            products.Add(product);
        }
        
        return products;
    }

    public async Task<Product> GetProductByIdAsync(int productId, CancellationToken ct = default)
    {
        using IDbConnection conn = Connection;
        conn.Open();
        var product = await conn.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Products WHERE ProductID = @ProductId", new { ProductId = productId });
        return product;
    }

    public async Task AddProductAsync(Product product, CancellationToken ct = default)
    {
        using IDbConnection conn = Connection;
        conn.Open();
        await conn.ExecuteAsync("INSERT INTO Products (Name, BasePrice) VALUES (@Name, @BasePrice)", new { Name = product.Name, BasePrice = product.BasePrice.Value });
    }

    public async Task DeleteProductAsync(int productId, CancellationToken ct = default)
    {
        using IDbConnection conn = Connection;
        conn.Open();
        await conn.ExecuteAsync("DELETE FROM Products WHERE ProductID = @ProductId", new { ProductId = productId });
    }
}