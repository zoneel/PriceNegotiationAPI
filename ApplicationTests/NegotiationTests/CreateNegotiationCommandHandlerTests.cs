using NSubstitute;
using PriceNegotiationAPI.Application.Negotiation.Command.Create;
using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Enums;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;

namespace ApplicationTests.NegotiationTests;

    public class CreateNegotiationCommandHandlerTests
    {
        [Fact]
        public async Task Handle_UserNegotiationDoesNotExist_AddsNegotiation()
        {
            // Arrange
            var negotiationRepository = Substitute.For<INegotiationRepository>();
            var productRepository = Substitute.For<IProductRepository>();
            var handler = new CreateNegotiationCommandHandler(negotiationRepository, productRepository);
            var command = new CreateNegotiationCommand(new AddNegotiationDto { ProductId = 123, ProposedPrice = 100 }, 1);

            negotiationRepository.GetUserNegotiationForProduct(1, 123)
                .Returns(Task.FromResult<Negotiation>(null)); // Simulating user negotiation does not exist

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            await negotiationRepository.Received(1).AddNegotiationAsync(Arg.Any<Negotiation>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_ProposedPriceGreaterThanTwiceBasePrice_ThrowsHighballOfferException()
        {
            // Arrange
            var negotiationRepository = Substitute.For<INegotiationRepository>();
            var productRepository = Substitute.For<IProductRepository>();
            var handler = new CreateNegotiationCommandHandler(negotiationRepository, productRepository);
            var product = new Product { ProductId = 123, BasePrice = 50 }; // Simulating a product with base price
            var command = new CreateNegotiationCommand(new AddNegotiationDto { ProductId = 123, ProposedPrice = 200 }, 1);

            negotiationRepository.GetUserNegotiationForProduct(1, 123)
                .Returns(new Negotiation { Status = OfferState.Pending, UserAttempts = 1, NegotiationId = 1 }); // Simulating user negotiation exists
            productRepository.GetProductByIdAsync(123)
                .Returns(product);

            // Act & Assert
            await Assert.ThrowsAsync<HighballOfferException>(() => handler.Handle(command, CancellationToken.None));
        }
    }