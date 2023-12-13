namespace PriceNegotiationAPI.Domain.Exceptions;

public class IdempotencyException : Exception
{
    private readonly int _statusCode = 409;
    public int statusCode => _statusCode;
    public IdempotencyException(string message) : base(message)
    {
    }
}