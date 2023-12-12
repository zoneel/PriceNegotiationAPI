using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.User.Dto;

namespace PriceNegotiationAPI.Application.User.Command.Register;

public record RegisterUserCommand(RegisterUserDto Dto) : ICommand;