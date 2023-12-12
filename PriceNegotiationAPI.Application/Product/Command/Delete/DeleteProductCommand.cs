using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Product.Command.Delete;

public record DeleteProductCommand(int ProductId) : ICommand;