using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;

namespace PriceNegotiationAPI.Application.Negotiation.Query.GetPending;

internal record GetPendingNegotiationsQuery() : IQuery<List<ShowNegotiationDto>>;
