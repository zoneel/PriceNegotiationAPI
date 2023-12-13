namespace PriceNegotiationAPI.Domain.Security;

public interface IPasswordManager
{
    string Secure(string password);
    bool ValidateAsync(string password, string securedPassword, CancellationToken token);
}