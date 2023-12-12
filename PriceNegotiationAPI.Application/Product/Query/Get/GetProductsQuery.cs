using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Product.Dto;

namespace PriceNegotiationAPI.Application.Product.Query.Get;

public record GetProductsQuery() : IQuery<IEnumerable<ProductDto>>;
