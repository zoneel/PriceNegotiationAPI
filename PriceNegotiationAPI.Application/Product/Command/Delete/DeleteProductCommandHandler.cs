using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Product.Command.Delete;

internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteProductAsync(request.ProductId, cancellationToken);
    }
}