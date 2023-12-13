namespace PriceNegotiationAPI.Domain.Exceptions;

public class ProductNotFoundException : Exception
{
    private readonly int _statusCode = 404;
    public int StatusCode => _statusCode;
    public ProductNotFoundException(string message) : base(message)
    {
    }
}