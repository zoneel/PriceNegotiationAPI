using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Application.Negotiation.Query.GetAll;

namespace PriceNegotiationAPI.Application.Negotiation.Query.GetPending;

internal class GetPendingNegotiationsQueryHandler : IQueryHandler<GetAllNegotiationsQuery, List<ShowNegotiationDto>>
{
    public Task<List<ShowNegotiationDto>> Handle(GetAllNegotiationsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}