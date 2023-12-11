using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Product.Dto;

namespace PriceNegotiationAPI.Application.Product.Query.Get;

internal record GetProductsQuery() : IQuery<List<ProductDto>>;
