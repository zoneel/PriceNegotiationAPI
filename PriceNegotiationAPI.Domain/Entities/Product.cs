using PriceNegotiationAPI.Domain.ValueObject;

namespace PriceNegotiationAPI.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Price BasePrice { get; set; }
}