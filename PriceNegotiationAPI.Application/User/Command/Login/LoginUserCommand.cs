using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.User.Dto;

namespace PriceNegotiationAPI.Application.User.Command.Login;

public record LoginUserCommand(LoginUserDto Dto) : ICommand;