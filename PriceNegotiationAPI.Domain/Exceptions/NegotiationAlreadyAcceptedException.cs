namespace PriceNegotiationAPI.Domain.Exceptions;

public class NegotiationAlreadyAcceptedException : Exception
{
    private readonly int _statusCode = 409;
    public int StatusCode => _statusCode;
    public NegotiationAlreadyAcceptedException(string message) : base(message)
    {
    }
}