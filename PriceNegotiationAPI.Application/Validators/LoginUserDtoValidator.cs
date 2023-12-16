using FluentValidation;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Validators;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    public LoginUserDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}