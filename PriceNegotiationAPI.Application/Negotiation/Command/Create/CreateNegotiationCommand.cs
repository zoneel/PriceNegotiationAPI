using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Create;

public record CreateNegotiationCommand(NegotiationDto dto) : ICommand;
