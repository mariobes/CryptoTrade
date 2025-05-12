using System.Security.Claims;
using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IAuthService
{
    User CheckLogin(string emailOrPhone, string password);
    public string GenerateJwtToken(User user);
    public bool HasAccessToResource(int? requestedUserID, string requestedUserEmailOrPhone, ClaimsPrincipal user);
}
