using Microsoft.AspNetCore.Http;

namespace PriceNegotiationAPI.Domain.Security;

public interface IUserIdentity
{ 
    int GetUserId(HttpContext userIdentity);
}