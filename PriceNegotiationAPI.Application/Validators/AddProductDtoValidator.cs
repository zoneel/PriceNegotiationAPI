using FluentValidation;
using PriceNegotiationAPI.Application.Product.Dto;

namespace PriceNegotiationAPI.Application.Validators;

public class AddProductDtoValidator : AbstractValidator<AddProductDto>
{
    public AddProductDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.BasePrice).NotEmpty().GreaterThan(0);
    }
}