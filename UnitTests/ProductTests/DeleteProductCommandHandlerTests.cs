using NSubstitute;
using PriceNegotiationAPI.Application.Product.Command.Delete;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;

namespace ApplicationTests.ProductTests;

public class DeleteProductCommandHandlerTests
{
    [Fact]
    public async Task Handle_Deletes_Product_When_Exists()
    {
        // Arrange
        var mockRepository = Substitute.For<IProductRepository>();
        var handler = new DeleteProductCommandHandler(mockRepository);
        var productId = 123; // Replace with an existing product ID
        var command = new DeleteProductCommand(productId);

        mockRepository.GetProductByIdAsync(productId, Arg.Any<CancellationToken>())
            .Returns(new Product()); // Simulate product exists

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await mockRepository.Received(1).DeleteProductAsync(productId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Throws_ProductNotFoundException_When_Product_Not_Found()
    {
        // Arrange
        var mockRepository = Substitute.For<IProductRepository>();
        var handler = new DeleteProductCommandHandler(mockRepository);
        var productId = 456; 
        var command = new DeleteProductCommand(productId);

        mockRepository.GetProductByIdAsync(productId, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Product>(null)); // Simulate product not found

        // Act & Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }
}