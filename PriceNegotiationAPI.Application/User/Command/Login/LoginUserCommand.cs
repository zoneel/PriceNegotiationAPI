using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Domain.Security;

namespace PriceNegotiationAPI.Application.User.Command.Login;

public record LoginUserCommand(LoginUserDto Dto) : ICommand<JwtToken>;