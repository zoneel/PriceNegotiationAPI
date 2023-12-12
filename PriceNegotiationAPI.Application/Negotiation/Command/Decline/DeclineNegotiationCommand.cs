using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Decline;

public record DeclineNegotiationCommand(int NegotiationId) : ICommand;
