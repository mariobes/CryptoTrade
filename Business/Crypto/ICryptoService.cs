using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface ICryptoService
{
    public Task UpdateCryptosDatabase(List<Crypto> cryptos);
    public Task UpdateCryptoRankDatabase();
    public Crypto RegisterCrypto(CryptoCreateUpdateDto dto);
    public IEnumerable<Crypto> GetAllCryptos(CryptoQueryParameters? dto = null);
    public Crypto GetCryptoById(string cryptoId);
    public void UpdateCrypto(string cryptoId, CryptoCreateUpdateDto dto);
    public void DeleteCrypto(string cryptoId);
    public List<Crypto> SearchCrypto(string query);
}
