using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Domain.Enums;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Decline;

internal class DeclineNegotiationCommandHandler : ICommandHandler<DeclineNegotiationCommand>
{
    private readonly INegotiationRepository _negotiationRepository;
    public DeclineNegotiationCommandHandler(INegotiationRepository negotiationRepository)
    {
        _negotiationRepository = negotiationRepository;
    }
    
    public async Task Handle(DeclineNegotiationCommand request, CancellationToken cancellationToken)
    {
        var negotiation = await _negotiationRepository.GetNegotiationByIdAsync(request.NegotiationId, cancellationToken);
        if (negotiation == null)
        {
            throw new ProductNotFoundException("Negotiation not found");
        }
        
        if(negotiation.Status == OfferState.Rejected)
            throw new IdempotencyException("Negotiation already rejected.");
        
        await _negotiationRepository.DeclineNegotiationAsync(request.NegotiationId, cancellationToken);
    }
}