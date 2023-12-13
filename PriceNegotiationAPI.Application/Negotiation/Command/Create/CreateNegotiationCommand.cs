using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Create;

public record CreateNegotiationCommand(AddNegotiationDto dto, int userId) : ICommand;
