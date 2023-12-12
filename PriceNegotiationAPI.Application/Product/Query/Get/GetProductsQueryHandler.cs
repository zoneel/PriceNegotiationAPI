using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Product.Dto;
using PriceNegotiationAPI.Application.Product.Mapping;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Product.Query.Get;

internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsAsync();
        var showProductDtos = ProductMapping.MapProductsToProductDtosEntity(products);
        return showProductDtos;
    }
}
