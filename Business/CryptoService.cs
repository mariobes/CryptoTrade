using CryptoTrade.Data;
using CryptoTrade.Models;

namespace CryptoTrade.Business;

public class CryptoService : ICryptoService
{
    private readonly ICryptoRepository _repository;

    public CryptoService(ICryptoRepository repository)
    {
        _repository = repository;
    }

    public Crypto RegisterCrypto(CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        var registeredCrypto = _repository.GetAllCryptos().FirstOrDefault(c => c.Name.Equals(cryptoCreateUpdateDTO.Name, StringComparison.OrdinalIgnoreCase));
        if (registeredCrypto != null)
        {
            throw new Exception("El nombre de la criptomoneda ya existe.");
        }
        Crypto crypto = new(cryptoCreateUpdateDTO.Name, cryptoCreateUpdateDTO.Symbol, cryptoCreateUpdateDTO.MarketCap, cryptoCreateUpdateDTO.Description, cryptoCreateUpdateDTO.Value, cryptoCreateUpdateDTO.Ranking, cryptoCreateUpdateDTO.Website, cryptoCreateUpdateDTO.TotalSupply, cryptoCreateUpdateDTO.CirculatingSupply, cryptoCreateUpdateDTO.Contract, cryptoCreateUpdateDTO.AllTimeHigh, cryptoCreateUpdateDTO.AllTimeLow);
        _repository.AddCrypto(crypto);
        return crypto;
    }

    public IEnumerable<Crypto> GetAllCryptos()
    {
        return _repository.GetAllCryptos();
    }

    public Crypto GetCryptoById(int cryptoId)
    {
        var crypto = _repository.GetCrypto(cryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoId} no encontrada");
        }
        return crypto;
    }

    public void UpdateCrypto(int cryptoId, CryptoCreateUpdateDTO cryptoCreateUpdateDTO)
    {
        var crypto = _repository.GetCrypto(cryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoId} no encontrada");
        }

        var registeredCrypto = _repository.GetAllCryptos().FirstOrDefault(c => c.Name.Equals(cryptoCreateUpdateDTO.Name, StringComparison.OrdinalIgnoreCase));
        if ((registeredCrypto != null) && (cryptoId != registeredCrypto.Id))
        {
            throw new Exception("El nombre de la criptomoneda ya existe.");
        }

        crypto.Name = cryptoCreateUpdateDTO.Name;
        crypto.Symbol = cryptoCreateUpdateDTO.Symbol;
        crypto.MarketCap = cryptoCreateUpdateDTO.MarketCap;
        crypto.Description = cryptoCreateUpdateDTO.Description;
        crypto.Value = cryptoCreateUpdateDTO.Value;
        crypto.Ranking = cryptoCreateUpdateDTO.Ranking;
        crypto.Website = cryptoCreateUpdateDTO.Website;
        crypto.TotalSupply = cryptoCreateUpdateDTO.TotalSupply;
        crypto.CirculatingSupply = cryptoCreateUpdateDTO.CirculatingSupply;
        crypto.Contract = cryptoCreateUpdateDTO.Contract;
        crypto.AllTimeHigh = cryptoCreateUpdateDTO.AllTimeHigh;
        crypto.AllTimeLow = cryptoCreateUpdateDTO.AllTimeLow;
        _repository.UpdateCrypto(crypto);
    }
    
}