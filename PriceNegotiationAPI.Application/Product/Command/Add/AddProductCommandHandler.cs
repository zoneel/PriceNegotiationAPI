using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Product.Mapping;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Product.Command.Add;

internal class AddProductCommandHandler : ICommandHandler<AddProductCommand>
{
    private readonly IProductRepository _productRepository;

    public AddProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = ProductMapping.MapAddProductDtoToProductEntity(request.Dto);
        await _productRepository.AddProductAsync(product, cancellationToken);
    }
}