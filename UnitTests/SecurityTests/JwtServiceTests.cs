using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PriceNegotiationAPI.Application.Security;
using PriceNegotiationAPI.Infrastructure.Security;

namespace ApplicationTests.SecurityTests;

public class JwtServiceTests
{
    [Fact]
    public async Task CreateTokenAsync_Generates_Token_With_Correct_Claims()
    {
        // Arrange
        var authOptions = new AuthOptions
        {
            Issuer = "testIssuer",
            Audience = "testAudience",
            SigningKey = Convert.ToBase64String(Encoding.UTF8.GetBytes("YourSecretKeyHere")),
            Expiry = TimeSpan.FromHours(1)
        };

        var options = Options.Create(authOptions);
        var jwtService = new JwtService(options);

        var userId = 123;
        var roleId = 456;

        // Act
        var jwtToken = await jwtService.CreateTokenAsync(userId, roleId);

        // Assert
        Assert.NotNull(jwtToken);
        Assert.NotEmpty(jwtToken.Token);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = authOptions.Issuer,
            ValidAudience = authOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SigningKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        var principal = tokenHandler.ValidateToken(jwtToken.Token, tokenValidationParams, out _);

        Assert.NotNull(principal);

        var claimsIdentity = principal.Identity as ClaimsIdentity;

        Assert.NotNull(claimsIdentity);
        Assert.Equal(userId.ToString(), claimsIdentity.FindFirst(JwtRegisteredClaimNames.Sid)?.Value);
        Assert.Equal(roleId.ToString(), claimsIdentity.FindFirst("Role")?.Value);
    }

}