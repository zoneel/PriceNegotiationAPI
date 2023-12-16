using NSubstitute;
using PriceNegotiationAPI.Application.Negotiation.Command.Decline;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Enums;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;

namespace ApplicationTests.NegotiationTests;

    public class DeclineNegotiationCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidNegotiationId_DeclinesNegotiation()
        {
            // Arrange
            var negotiationRepository = Substitute.For<INegotiationRepository>();
            var handler = new DeclineNegotiationCommandHandler(negotiationRepository);
            var command = new DeclineNegotiationCommand(123);

            negotiationRepository.GetNegotiationByIdAsync(123, Arg.Any<CancellationToken>())
                .Returns(new Negotiation { NegotiationId = 123, Status = OfferState.Pending, UserAttempts = 2 }); // Simulating existing negotiation

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            await negotiationRepository.Received(1).DeclineNegotiationAsync(123, 2, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_NegotiationAlreadyRejected_ThrowsIdempotencyException()
        {
            // Arrange
            var negotiationRepository = Substitute.For<INegotiationRepository>();
            var handler = new DeclineNegotiationCommandHandler(negotiationRepository);
            var command = new DeclineNegotiationCommand(123);

            negotiationRepository.GetNegotiationByIdAsync(123, Arg.Any<CancellationToken>())
                .Returns(new Negotiation { NegotiationId = 123, Status = OfferState.Rejected }); // Simulating already rejected negotiation

            // Act & Assert
            await Assert.ThrowsAsync<IdempotencyException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NegotiationAlreadyAccepted_ThrowsNegotiationAlreadyAcceptedException()
        {
            // Arrange
            var negotiationRepository = Substitute.For<INegotiationRepository>();
            var handler = new DeclineNegotiationCommandHandler(negotiationRepository);
            var command = new DeclineNegotiationCommand(123);

            negotiationRepository.GetNegotiationByIdAsync(123, Arg.Any<CancellationToken>())
                .Returns(new Negotiation { NegotiationId = 123, Status = OfferState.Accepted }); // Simulating already accepted negotiation

            // Act & Assert
            await Assert.ThrowsAsync<NegotiationAlreadyAcceptedException>(() => handler.Handle(command, CancellationToken.None));
        }
    }