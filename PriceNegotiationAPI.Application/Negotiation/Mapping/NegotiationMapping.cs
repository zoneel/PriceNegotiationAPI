using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Domain.ValueObject;

namespace PriceNegotiationAPI.Application.Negotiation.Mapping;

public class NegotiationMapping
{
    public static Domain.Entities.Negotiation MapAddNegotiationDtotoNegotiationEntity(AddNegotiationDto addNegotiation, int userId)
    {
        var negotiation = new Domain.Entities.Negotiation
        {
            ProductId = addNegotiation.ProductId,
            ProposedPrice = addNegotiation.ProposedPrice,
            UserAttempts = 1,
            Status = 0,
            CreatorUserId = userId
        };
        return negotiation;
    }
}