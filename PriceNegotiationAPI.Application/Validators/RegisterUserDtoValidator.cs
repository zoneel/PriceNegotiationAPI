using FluentValidation;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserDtoValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.Role).Must(x => x == 0 || x == 1).WithMessage("Role must be either 0 or 1");

        RuleFor(x => x.Email).MustAsync(async (email, cancellation) => !await EmailExists(email))
            .WithMessage("Email already exists. Please use a different email.");

        RuleFor(x => x.Name).MustAsync(async (name, cancellation) => !await NameExists(name))
            .WithMessage("Name already exists. Please use a different name.");
    }

    private async Task<bool> EmailExists(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user != null; // Returns true if the user exists
    }

    private async Task<bool> NameExists(string name)
    {
        var user = await _userRepository.GetUserByNameAsync(name);
        return user != null; // Returns true if the user exists
    }
}