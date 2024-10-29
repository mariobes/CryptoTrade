using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface ICryptoService
{
    public IEnumerable<Crypto> GetAllCryptos();
}
