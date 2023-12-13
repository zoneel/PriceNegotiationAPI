using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Product.Dto;

namespace PriceNegotiationAPI.Application.Product.Command.Add;

public record AddProductCommand(AddProductDto Dto) : ICommand;