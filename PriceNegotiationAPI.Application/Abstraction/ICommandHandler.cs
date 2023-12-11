using MediatR;

namespace PriceNegotiationAPI.Application.Abstraction;

internal interface ICommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
{
}


internal interface ICommandHandler<TCommand, TResponse> 
    : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
}