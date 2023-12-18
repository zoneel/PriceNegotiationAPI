using NSubstitute;
using PriceNegotiationAPI.Application.Product.Command.Add;
using PriceNegotiationAPI.Application.Product.Dto;
using PriceNegotiationAPI.Application.Product.Mapping;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Repository;

namespace ApplicationTests.ProductTests;

    public class AddProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Calls_AddProductAsync_With_Correct_Arguments()
        {
            // Arrange
            var mockRepository = Substitute.For<IProductRepository>();
            var handler = new AddProductCommandHandler(mockRepository);
            var command = new AddProductCommand(new AddProductDto
            {
                Name = "Test Product",
                BasePrice = 100
            });

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            await mockRepository.Received(1).AddProductAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }
        
    }
