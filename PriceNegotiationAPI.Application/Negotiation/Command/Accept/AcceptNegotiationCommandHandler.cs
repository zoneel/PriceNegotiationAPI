using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Domain.Enums;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Accept;

internal class AcceptNegotiationCommandHandler : ICommandHandler<AcceptNegotiationCommand>
{
private readonly INegotiationRepository _negotiationRepository;
    public AcceptNegotiationCommandHandler(INegotiationRepository negotiationRepository)
    {
        _negotiationRepository = negotiationRepository;
    }
    
    public async Task Handle(AcceptNegotiationCommand request, CancellationToken cancellationToken)
    {
        var negotiation = await _negotiationRepository.GetNegotiationByIdAsync(request.NegotiationId, cancellationToken);
        if (negotiation == null)
        {
            throw new ProductNotFoundException("Negotiation not found");
        }
        
        if(negotiation.Status == OfferState.Accepted)
            throw new IdempotencyException("Negotiation already accepted");
        
        await _negotiationRepository.AcceptNegotiationAsync(request.NegotiationId, cancellationToken);
    }
}