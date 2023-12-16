namespace PriceNegotiationAPI.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
}