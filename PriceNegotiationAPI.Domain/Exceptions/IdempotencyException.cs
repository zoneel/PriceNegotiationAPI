namespace PriceNegotiationAPI.Domain.Exceptions;

public class IdempotencyException : Exception
{
    private readonly int _statusCode = 409;
    public int StatusCode => _statusCode;
    public IdempotencyException(string message) : base(message)
    {
    }
}