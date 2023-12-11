using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.User.Dto;

namespace PriceNegotiationAPI.Application.User.Command.Register;

internal record RegisterUserCommand(RegisterUserDto Dto) : ICommand;