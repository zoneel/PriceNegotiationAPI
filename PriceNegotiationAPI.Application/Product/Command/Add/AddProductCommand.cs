using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Product.Dto;

namespace PriceNegotiationAPI.Application.Product.Command.Add;

internal record AddProductCommand(ProductDto Dto) : ICommand;