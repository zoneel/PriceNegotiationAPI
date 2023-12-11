using MediatR;

namespace PriceNegotiationAPI.Application.Abstraction;

internal interface ICommand : IRequest {}

internal interface ICommand<TResponse> : IRequest<TResponse> {}
