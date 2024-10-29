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

    public IEnumerable<Crypto> GetAllCryptos()
    {
        return _repository.GetAllCryptos();
    }
    
}