using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Command.Decline;
using PriceNegotiationAPI.Application.Negotiation.Dto;

namespace PriceNegotiationAPI.Application.Negotiation.Query.GetAll;

internal class GetAllNegotiationsQueryHandler : IQueryHandler<GetAllNegotiationsQuery, List<ShowNegotiationDto>>
{
    public Task<List<ShowNegotiationDto>> Handle(GetAllNegotiationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}