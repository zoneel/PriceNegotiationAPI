using PriceNegotiationAPI.Domain.Entities;
using PriceNegotiationAPI.Domain.Enums;

namespace PriceNegotiationAPI.Domain.Tests;

public class NegotiationTests
{
    [Fact]
    public void Negotiation_GettersAndSetters_WorkCorrectly()
    {
        // Arrange
        var negotiation = new Negotiation();

        // Act
        negotiation.NegotiationId = 1;
        negotiation.ProductId = 2;
        negotiation.ProposedPrice = 100.0m;
        negotiation.UserAttempts = 3;
        negotiation.Status = OfferState.Pending;
        negotiation.CreatorUserId = 4;

        // Assert
        Assert.Equal(1, negotiation.NegotiationId);
        Assert.Equal(2, negotiation.ProductId);
        Assert.Equal(100.0m, negotiation.ProposedPrice);
        Assert.Equal(3, negotiation.UserAttempts);
        Assert.Equal(OfferState.Pending, negotiation.Status);
        Assert.Equal(4, negotiation.CreatorUserId);
    }
}