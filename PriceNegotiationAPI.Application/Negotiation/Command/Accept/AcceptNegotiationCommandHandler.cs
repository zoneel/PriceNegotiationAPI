using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Accept;

internal class AcceptNegotiationCommandHandler : ICommandHandler<AcceptNegotiationCommand>
{
    public Task Handle(AcceptNegotiationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}