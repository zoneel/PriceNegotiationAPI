using FluentValidation;
using PriceNegotiationAPI.Application.User.Dto;
using PriceNegotiationAPI.Domain.Repository;

namespace PriceNegotiationAPI.Application.Validators;

public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    private readonly IUserRepository _userRepository;

    public LoginUserDtoValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();

        //RuleFor(x => x.Email).MustAsync(async (email, cancellation) => !await UserExists(email))
          //  .WithMessage("Email already exists. Please use a different email.");
    }

    private async Task<bool> UserExists(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        return user == null; 
    }
}