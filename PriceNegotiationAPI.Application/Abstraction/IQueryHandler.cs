using MediatR;

namespace PriceNegotiationAPI.Application.Abstraction;

internal interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
{
}