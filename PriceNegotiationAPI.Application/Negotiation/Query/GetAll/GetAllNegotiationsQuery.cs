using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;

namespace PriceNegotiationAPI.Application.Negotiation.Query.GetAll;

internal record GetAllNegotiationsQuery() : IQuery<List<ShowNegotiationDto>>;
