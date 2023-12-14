using PriceNegotiationAPI.Domain.Enums;

namespace PriceNegotiationAPI.Domain.Entities;

public class Negotiation
{
    public int NegotiationId { get; set; }
    public int ProductId { get; set; }
    public decimal ProposedPrice { get; set; }
    public int UserAttempts { get; set; }
    public OfferState Status { get; set; }
    public int CreatorUserId { get; set; }
}