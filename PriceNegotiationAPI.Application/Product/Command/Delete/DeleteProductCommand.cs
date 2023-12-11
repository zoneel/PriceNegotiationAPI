using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Product.Command.Delete;

internal record DeleteProductCommand(int ProductId) : ICommand;