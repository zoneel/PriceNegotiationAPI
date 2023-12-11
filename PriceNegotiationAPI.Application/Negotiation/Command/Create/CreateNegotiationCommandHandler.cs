using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Command.Accept;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Create;

internal class CreateNegotiationCommandHandler : ICommandHandler<CreateNegotiationCommand>
{
    public Task Handle(CreateNegotiationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}