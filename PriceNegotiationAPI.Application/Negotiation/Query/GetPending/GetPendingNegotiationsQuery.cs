using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;

namespace PriceNegotiationAPI.Application.Negotiation.Query.GetPending;

public record GetPendingNegotiationsQuery() : IQuery<List<ShowNegotiationDto>>;
