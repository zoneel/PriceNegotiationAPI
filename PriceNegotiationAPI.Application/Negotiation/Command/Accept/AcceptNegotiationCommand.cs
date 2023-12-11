using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Accept;

internal record AcceptNegotiationCommand(int NegotiationId) : ICommand;