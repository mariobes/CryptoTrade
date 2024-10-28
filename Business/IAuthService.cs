using CryptoTrade.Models;

namespace CryptoTrade.Business
{
    public interface IAuthService
    {
        User CheckLogin(string email, string password);
    }
}
