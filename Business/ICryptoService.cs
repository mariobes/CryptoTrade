using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface ICryptoService
{
    public Task UpdateCryptosDatabase(List<Crypto> cryptos);
    public Crypto RegisterCrypto(CryptoCreateUpdateDTO dto);
    public IEnumerable<Crypto> GetAllCryptos(CryptoQueryParameters? dto = null);
    public Crypto GetCryptoById(string cryptoId);
    public void UpdateCrypto(string cryptoId, CryptoCreateUpdateDTO dto);
    public void DeleteCrypto(string cryptoId);
}
