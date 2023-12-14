using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Command.Decline;
using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Application.Negotiation.Mapping;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Negotiation.Query.GetAll;

internal class GetAllNegotiationsQueryHandler : IQueryHandler<GetAllNegotiationsQuery, List<ShowNegotiationDto>>
{
    private readonly INegotiationRepository _negotiationRepository;
    public GetAllNegotiationsQueryHandler(INegotiationRepository negotiationRepository)
    {
        _negotiationRepository = negotiationRepository;
    }
    public async Task<List<ShowNegotiationDto>> Handle(GetAllNegotiationsQuery request, CancellationToken cancellationToken)
    {
        var negotiations = await _negotiationRepository.GetAllNegotiationsAsync();

        var showNegotiations = new List<ShowNegotiationDto>();
        foreach (var negotiation in negotiations)
        {
            var mappedNegotiation = NegotiationMapping.MapNegotiationEntityToShowNegotiationDto(negotiation);
            showNegotiations.Add(mappedNegotiation);
        }
        
        return showNegotiations;
    }
}