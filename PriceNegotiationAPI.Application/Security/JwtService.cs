using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PriceNegotiationAPI.Domain.Security;
using PriceNegotiationAPI.Infrastructure.Security;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace PriceNegotiationAPI.Application.Security;

public class JwtService : IJwtService
{
    private readonly string _issuer;
    private readonly TimeSpan _expiry;
    private readonly string _audience;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new JwtSecurityTokenHandler();


    public JwtService(IOptions<AuthOptions> options)
    {
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(options.Value.SigningKey)),
            SecurityAlgorithms.HmacSha256);
    }

    public async Task<JwtToken> CreateTokenAsync(int UserId, int roleId, CancellationToken ct = default)
    {
        var now = DateTime.Now;
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sid, UserId.ToString()),
            new("Role", roleId.ToString()),
        };

        var expires = now.Add(_expiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return new JwtToken()
        {
            Token = token
        };
    }
}