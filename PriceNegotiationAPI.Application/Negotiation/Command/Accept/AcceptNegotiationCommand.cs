using PriceNegotiationAPI.Application.Abstraction;

namespace PriceNegotiationAPI.Application.Negotiation.Command.Accept;

public record AcceptNegotiationCommand(int NegotiationId) : ICommand;