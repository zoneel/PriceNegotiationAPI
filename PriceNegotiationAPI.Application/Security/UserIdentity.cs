using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using PriceNegotiationAPI.Domain.Security;

namespace PriceNegotiationAPI.Application.Security;

public class UserIdentity : IUserIdentity
{
    public int GetUserId(HttpContext httpContext)
    {
        if (httpContext.User.Identity.IsAuthenticated && httpContext.User.FindFirst(JwtRegisteredClaimNames.Sid) != null)
        {
            return Convert.ToInt32(httpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        }
        throw new UnauthorizedAccessException("User ID not found or user not authenticated.");
    }
}