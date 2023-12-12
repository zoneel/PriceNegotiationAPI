using FluentValidation;

namespace PriceNegotiationAPI.Application.Validators;

public class IntValidator  : AbstractValidator<int>
{
    public IntValidator()
    {
        RuleFor(x => x).NotEmpty().GreaterThan(0);
    }
}