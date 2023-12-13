using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Domain.Exceptions;
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
        var productToDelete = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);
        if (productToDelete is null)
        {
            throw new ProductNotFoundException($"Product with ID {request.ProductId} was not found.");
        }
        
        await _productRepository.DeleteProductAsync(request.ProductId, cancellationToken);
    }
}