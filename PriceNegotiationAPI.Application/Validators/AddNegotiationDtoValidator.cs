using FluentValidation;
using PriceNegotiationAPI.Application.Negotiation.Dto;
using PriceNegotiationAPI.Application.Product.Dto;

namespace PriceNegotiationAPI.Application.Validators;

public class AddNegotiationDtoValidator : AbstractValidator<AddNegotiationDto>
{
    public AddNegotiationDtoValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id is required");
        RuleFor(x => x.ProposedPrice).NotEmpty().WithMessage("Proposed Price is required");
    }
}