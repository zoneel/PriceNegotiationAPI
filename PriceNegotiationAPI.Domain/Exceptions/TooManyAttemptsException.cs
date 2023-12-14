namespace PriceNegotiationAPI.Domain.Exceptions;

public class TooManyAttemptsException : Exception
{
    private readonly int _statusCode = 429;
    public int StatusCode => _statusCode;
    public TooManyAttemptsException(string message) : base(message)
    {
    }
}