using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.User.Command.Login;
using PriceNegotiationAPI.Domain.Repository;
using PriceNegotiationAPI.Domain.Security;

namespace PriceNegotiationAPI.Application.User.Command.Register;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
    }
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordManager.Secure(request.Dto.Password);

        var newUser = new Domain.Entities.User
        {
            Name = request.Dto.Name,
            Email = request.Dto.Email,
            Password = hashedPassword,
            Role = request.Dto.Role
        };

        await _userRepository.AddUserAsync(newUser);
    }
}