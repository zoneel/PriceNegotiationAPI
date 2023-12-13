namespace PriceNegotiationAPI.Domain.Exceptions;

public class HighballOfferException : Exception
{
    private readonly int _statusCode = 406;
    public int StatusCode => _statusCode;
    public HighballOfferException(string message) : base(message)
    {
    }
}