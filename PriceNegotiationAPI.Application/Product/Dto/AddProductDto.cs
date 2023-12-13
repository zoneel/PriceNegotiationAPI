namespace PriceNegotiationAPI.Application.Product.Dto;

public class AddProductDto
{
    public string Name { get; set; } = null!;
    public decimal BasePrice { get; set; }
}