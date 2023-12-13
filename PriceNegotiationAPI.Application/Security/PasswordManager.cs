using Microsoft.AspNetCore.Identity;
using PriceNegotiationAPI.Domain.Security;

namespace PriceNegotiationAPI.Application.Security;

public class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;

    public PasswordManager(IPasswordHasher<Domain.Entities.User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password) => _passwordHasher.HashPassword(default, password);

    public bool ValidateAsync(string password, string securedPassword, CancellationToken token)
        => _passwordHasher.VerifyHashedPassword(default, securedPassword, password) ==
           PasswordVerificationResult.Success;
}