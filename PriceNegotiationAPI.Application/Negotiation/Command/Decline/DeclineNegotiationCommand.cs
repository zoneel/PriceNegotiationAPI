using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Decline;

internal record DeclineNegotiationCommand(int NegotiationId) : ICommand;
