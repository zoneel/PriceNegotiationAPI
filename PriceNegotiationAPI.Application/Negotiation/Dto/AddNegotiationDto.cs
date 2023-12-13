using PriceNegotiationAPI.Domain.Enums;

namespace PriceNegotiationAPI.Application.Negotiation.Dto;

public class AddNegotiationDto
{
    public int ProductId { get; set; }
    public decimal ProposedPrice { get; set; }
}