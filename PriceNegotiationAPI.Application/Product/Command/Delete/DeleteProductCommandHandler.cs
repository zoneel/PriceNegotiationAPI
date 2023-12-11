using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Product.Command.Delete;

internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    public Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}