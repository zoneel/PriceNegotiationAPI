namespace PriceNegotiationAPI.Domain.Security;

public interface IJwtService
{
    public Task<JwtToken> CreateTokenAsync(int UserId, int roleId, CancellationToken token = default);

}