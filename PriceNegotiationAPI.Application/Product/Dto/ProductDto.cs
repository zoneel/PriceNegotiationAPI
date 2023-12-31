﻿namespace PriceNegotiationAPI.Application.Product.Dto;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal BasePrice { get; set; }
}