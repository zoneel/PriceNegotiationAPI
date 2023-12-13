namespace PriceNegotiationAPI.Domain.Exceptions;

public class ProductNotFoundException : Exception
{
    private readonly int _statusCode = 404;
    public int statusCode => _statusCode;
    public ProductNotFoundException(string message) : base(message)
    {
    }
}