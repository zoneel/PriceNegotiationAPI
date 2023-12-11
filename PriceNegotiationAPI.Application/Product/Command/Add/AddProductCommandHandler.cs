using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Product.Command.Add;

internal class AddProductCommandHandler : ICommandHandler<AddProductCommand>
{
    public Task Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}