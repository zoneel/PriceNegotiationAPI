using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Domain.Enums;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Decline;

public class DeclineNegotiationCommandHandler : ICommandHandler<DeclineNegotiationCommand>
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
            throw new ProductNotFoundException("Negotiation not found");
        
        switch (negotiation.Status)
        {
            case OfferState.Rejected:
                throw new IdempotencyException("Negotiation already rejected.");

            case OfferState.Accepted:
                throw new NegotiationAlreadyAcceptedException("Negotiation already accepted. Once accepted, it cannot be rejected by this endpoint.");
        }

        if (negotiation.UserAttempts > 3)
            throw new TooManyAttemptsException("You have already bumped this negotiation more than 3 times.");
        
        await _negotiationRepository.DeclineNegotiationAsync(request.NegotiationId, negotiation.UserAttempts, cancellationToken);
    }
}