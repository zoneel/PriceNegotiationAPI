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
            UserAttempts = 0,
            Status = 0,
            CreatorUserId = userId
        };
        return negotiation;
    }
    
    public static ShowNegotiationDto MapNegotiationEntityToShowNegotiationDto(Domain.Entities.Negotiation negotiation)
    {
        var showNegotiation = new ShowNegotiationDto
        {
            NegotiationId = negotiation.NegotiationId,
            ProductId = negotiation.ProductId,
            ProposedPrice = negotiation.ProposedPrice,
            UserAttempts = negotiation.UserAttempts,
            Status = negotiation.Status,
            CreatorUserId = negotiation.CreatorUserId
        };
        return showNegotiation;
    }
}