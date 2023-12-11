using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Application.Product.Dto;

namespace PriceNegotiationAPI.Application.Product.Query.Get;

internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, List<ProductDto>>
{
    public Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}