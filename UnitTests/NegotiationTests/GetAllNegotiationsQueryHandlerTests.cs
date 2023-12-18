using NSubstitute;
using PriceNegotiationAPI.Application.Negotiation.Query.GetAll;
using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Repository;

namespace ApplicationTests.NegotiationTests;

public class GetAllNegotiationsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllNegotiations()
    {
        // Arrange
        var negotiationRepository = Substitute.For<INegotiationRepository>();
        var handler = new GetAllNegotiationsQueryHandler(negotiationRepository);
        var query = new GetAllNegotiationsQuery();

        var negotiations = new List<Negotiation>
        {
            new Negotiation { NegotiationId = 1, ProductId = 1, ProposedPrice = 30, Status = 0, UserAttempts = 2, CreatorUserId = 1},
            new Negotiation { NegotiationId = 1, ProductId = 2, ProposedPrice = 532, Status = 0, UserAttempts = 1, CreatorUserId = 1},
        };

        negotiationRepository.GetAllNegotiationsAsync().Returns(negotiations);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(negotiations.Count, result.Count);
    }
}