using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Product.Command.Add;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Domain.Exceptions;
using PriceNegotiationAPI.Domain.Repository;
using PriceNegotiationAPI.Domain.Security;

namespace PriceNegotiationAPI.Application.User.Command.Login;

internal class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, JwtToken>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(IUserRepository userRepository, IPasswordManager passwordManager, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _jwtService = jwtService;
    }
    public async Task<JwtToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Dto.Email, cancellationToken);
        if(user.Email == null) throw new InvalidCredentialsException("Invalid email or password");
        
        if (!_passwordManager.ValidateAsync(request.Dto.Password, user.Password, cancellationToken)) 
            throw new InvalidCredentialsException("Invalid username or password");

        var token = await _jwtService.CreateTokenAsync(user.UserId, user.Role);
        return token;
    }
}