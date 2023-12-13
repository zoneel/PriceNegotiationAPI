namespace PriceNegotiationAPI.Domain.Exceptions;

public class InvalidCredentialsException : Exception
{
    private readonly int _statusCode = 401;
    public int StatusCode => _statusCode;
    public InvalidCredentialsException(string message) : base(message)
    {
    }
}