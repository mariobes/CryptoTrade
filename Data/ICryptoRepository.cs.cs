using CryptoTrade.Models;

namespace CryptoTrade.Data;

public interface ICryptoRepository
{
    public void AddCrypto(Crypto crypto);
    public Crypto GetCrypto(int cryptoId);
    IEnumerable<Crypto> GetAllCryptos();
    public void UpdateCrypto(Crypto crypto);
    public void DeleteCrypto(int cryptoId);
    void SaveChanges();
}
