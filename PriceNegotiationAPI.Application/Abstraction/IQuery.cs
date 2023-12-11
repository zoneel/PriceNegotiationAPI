using MediatR;

namespace PriceNegotiationAPI.Application.Abstraction;

internal interface IQuery<TResponse> : IRequest<TResponse>
{
}