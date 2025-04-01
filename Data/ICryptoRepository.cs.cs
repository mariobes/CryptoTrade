using CryptoTrade.Models;

namespace CryptoTrade.Data;

public interface ICryptoRepository
{
    public void AddCrypto(Crypto crypto);
    public Crypto GetCrypto(string cryptoId);
    IEnumerable<Crypto> GetAllCryptos(CryptoQueryParameters? dto = null);
    public void UpdateCrypto(Crypto crypto);
    public void DeleteCrypto(string cryptoId);
    void SaveChanges();
}
