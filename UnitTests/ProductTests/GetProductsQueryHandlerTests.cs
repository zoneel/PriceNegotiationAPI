using NSubstitute;
using PriceNegotiationAPI.Application.Product.Query.Get;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Repository;

namespace ApplicationTests.ProductTests;

public class GetProductsQueryHandlerTests
{
    [Fact]
    public async Task Handle_Returns_ProductDtos_When_Repository_Not_Empty()
    {
        // Arrange
        var mockRepository = Substitute.For<IProductRepository>();
        var handler = new GetProductsQueryHandler(mockRepository);

        var products = new List<Product>
        {
            new Product(){ ProductId = 1, Name = "Product 1", BasePrice = 100 },
            new Product(){ ProductId = 2, Name = "Product 2", BasePrice = 100 },
            new Product(){ ProductId = 3, Name = "Product 3", BasePrice = 100 },
        };

        mockRepository.GetAllProductsAsync()
            .Returns(Task.FromResult<IEnumerable<Product>>(products));

        // Act
        var result = await handler.Handle(new GetProductsQuery(), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Handle_Returns_Empty_When_Repository_Empty()
    {
        // Arrange
        var mockRepository = Substitute.For<IProductRepository>();
        var handler = new GetProductsQueryHandler(mockRepository);

        mockRepository.GetAllProductsAsync()
            .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));

        // Act
        var result = await handler.Handle(new GetProductsQuery(), CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }

}