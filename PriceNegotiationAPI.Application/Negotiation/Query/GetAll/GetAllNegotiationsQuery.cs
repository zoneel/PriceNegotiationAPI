using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;

namespace PriceNegotiationAPI.Application.Negotiation.Query.GetAll;

public record GetAllNegotiationsQuery() : IQuery<List<ShowNegotiationDto>>;
