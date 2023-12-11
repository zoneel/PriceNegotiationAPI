using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.User.Command.Login;

namespace PriceNegotiationAPI.Application.User.Command.Register;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    public Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}