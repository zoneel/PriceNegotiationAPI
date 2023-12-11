using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Product.Command.Add;

namespace PriceNegotiationAPI.Application.User.Command.Login;

internal class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
{
    public Task Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}