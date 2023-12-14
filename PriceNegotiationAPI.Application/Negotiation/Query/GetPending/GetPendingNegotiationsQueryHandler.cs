using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Application.Negotiation.Mapping;
using PriceNegotiationAPI.Application.Negotiation.Query.GetAll;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Negotiation.Query.GetPending;

internal class GetPendingNegotiationsQueryHandler : IQueryHandler<GetPendingNegotiationsQuery, List<ShowNegotiationDto>>
{
    private readonly INegotiationRepository _negotiationRepository;
    public GetPendingNegotiationsQueryHandler(INegotiationRepository negotiationRepository)
    {
        _negotiationRepository = negotiationRepository;
    }
    public async Task<List<ShowNegotiationDto>> Handle(GetPendingNegotiationsQuery request, CancellationToken cancellationToken)
    {
        var negotiations = await _negotiationRepository.GetAllPendingNegotiationsAsync();

        var showNegotiations = new List<ShowNegotiationDto>();
        foreach (var negotiation in negotiations)
        {
            var mappedNegotiation = NegotiationMapping.MapNegotiationEntityToShowNegotiationDto(negotiation);
            showNegotiations.Add(mappedNegotiation);
        }
        
        return showNegotiations;
    }
}