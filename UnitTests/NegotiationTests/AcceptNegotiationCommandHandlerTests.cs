using NSubstitute;
using PriceNegotiationAPI.Application.Negotiation.Command.Accept;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Enums;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;

namespace ApplicationTests.NegotiationTests;

public class AcceptNegotiationCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidNegotiationId_AcceptsNegotiation()
        {
            // Arrange
            var negotiationRepository = Substitute.For<INegotiationRepository>();
            var handler = new AcceptNegotiationCommandHandler(negotiationRepository);
            var command = new AcceptNegotiationCommand(123);

            negotiationRepository.GetNegotiationByIdAsync(123, Arg.Any<CancellationToken>())
                .Returns(new Negotiation { NegotiationId = 123, Status = OfferState.Pending });

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            await negotiationRepository.Received(1).AcceptNegotiationAsync(123, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_NegotiationNotFound_ThrowsProductNotFoundException()
        {
            // Arrange
            var negotiationRepository = Substitute.For<INegotiationRepository>();
            var handler = new AcceptNegotiationCommandHandler(negotiationRepository);
            var command = new AcceptNegotiationCommand(123);

            negotiationRepository.GetNegotiationByIdAsync(123, Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<Negotiation>(null)); // Simulating negotiation not found

            // Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NegotiationAlreadyAccepted_ThrowsIdempotencyException()
        {
            // Arrange
            var negotiationRepository = Substitute.For<INegotiationRepository>();
            var handler = new AcceptNegotiationCommandHandler(negotiationRepository);
            var command = new AcceptNegotiationCommand(123);

            negotiationRepository.GetNegotiationByIdAsync(123, Arg.Any<CancellationToken>())
                .Returns(new Negotiation { NegotiationId = 123, Status = OfferState.Accepted }); // Simulating already accepted

            // Act & Assert
            await Assert.ThrowsAsync<IdempotencyException>(() => handler.Handle(command, CancellationToken.None));
        }
    }