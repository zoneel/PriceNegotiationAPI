using NSubstitute;
using PriceNegotiationAPI.Application.Negotiation.Query.GetPending;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Enums;
using PriceNegotiationAPI.Domain.Repository;

namespace ApplicationTests.NegotiationTests;

    public class GetPendingNegotiationsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsPendingNegotiations()
        {
            // Arrange
            var negotiationRepository = Substitute.For<INegotiationRepository>();
            var handler = new GetPendingNegotiationsQueryHandler(negotiationRepository);
            var query = new GetPendingNegotiationsQuery();

            var pendingNegotiations = new List<Negotiation>
            {
                new Negotiation { NegotiationId = 1, Status = OfferState.Pending,},
                new Negotiation { NegotiationId = 2, Status = OfferState.Pending, },
                new Negotiation { NegotiationId = 3, Status = OfferState.Accepted, },
                new Negotiation { NegotiationId = 4, Status = OfferState.Rejected, },
            };

            negotiationRepository.GetAllPendingNegotiationsAsync().Returns(pendingNegotiations);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(pendingNegotiations.Count, result.Count);
                    }
    }
