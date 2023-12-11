using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Decline;

internal class DeclineNegotiationCommandHandler : ICommandHandler<DeclineNegotiationCommand>
{
    public Task Handle(DeclineNegotiationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}