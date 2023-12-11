using PriceNegotiationAPI.Application.Abstraction;
using PriceNegotiationAPI.Application.Negotiation.Dto;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Create;

internal record CreateNegotiationCommand(NegotiationDto dto) : ICommand;
