namespace PriceNegotiationAPI.Domain.ValueObject;

public class Price
{
    public decimal Value { get; private set; }

    public Price(decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Price must be greater than zero.");
        }

        Value = value;
    }
}