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

    public async Task UpdateCryptosDatabase(List<Crypto> cryptos)
    {
        foreach (var crypto in cryptos)
        {
            var registeredCrypto = _repository.GetAllCryptos().FirstOrDefault(c => c.Name.Equals(crypto.Name, StringComparison.OrdinalIgnoreCase));
            if (registeredCrypto != null)
            {
                registeredCrypto.Price = crypto.Price;
                registeredCrypto.MarketCap = crypto.MarketCap;
                registeredCrypto.MarketCapRank = null;
                registeredCrypto.FullyDilutedValuation = crypto.FullyDilutedValuation;
                registeredCrypto.TotalVolume = crypto.TotalVolume;
                registeredCrypto.High24h = crypto.High24h;
                registeredCrypto.Low24h = crypto.Low24h;
                registeredCrypto.PriceChange24h = crypto.PriceChange24h;
                registeredCrypto.PriceChangePercentage24h = crypto.PriceChangePercentage24h;
                registeredCrypto.PriceChangePercentage1h = crypto.PriceChangePercentage1h;
                registeredCrypto.PriceChangePercentage7d = crypto.PriceChangePercentage7d;
                registeredCrypto.MarketCapChange24h = crypto.MarketCapChange24h;
                registeredCrypto.MarketCapChangePercentage24h = crypto.MarketCapChangePercentage24h;
                registeredCrypto.CirculatingSupply = crypto.CirculatingSupply;
                registeredCrypto.TotalSupply = crypto.TotalSupply;
                registeredCrypto.MaxSupply = crypto.MaxSupply;
                registeredCrypto.AllTimeHigh = crypto.AllTimeHigh;
                registeredCrypto.AllTimeHighChangePercentage = crypto.AllTimeHighChangePercentage;
                registeredCrypto.AllTimeHighDate = crypto.AllTimeHighDate;
                registeredCrypto.AllTimeLow = crypto.AllTimeLow;
                registeredCrypto.AllTimeLowChangePercentage = crypto.AllTimeLowChangePercentage;
                registeredCrypto.AllTimeLowDate = crypto.AllTimeLowDate;
                registeredCrypto.SparklineIn7d = crypto.SparklineIn7d;
                registeredCrypto.LastUpdated = DateTime.UtcNow.AddHours(2);
                _repository.UpdateCrypto(registeredCrypto);
            }
            else
            {
                var newCrypto = new Crypto
                {
                    Id = crypto.Id,
                    Name = crypto.Name,
                    Symbol = crypto.Symbol,
                    Image = crypto.Image,
                    Price = crypto.Price,
                    MarketCap = crypto.MarketCap,
                    MarketCapRank = null,
                    FullyDilutedValuation = crypto.FullyDilutedValuation,
                    TotalVolume = crypto.TotalVolume,
                    High24h = crypto.High24h,
                    Low24h = crypto.Low24h,
                    PriceChange24h = crypto.PriceChange24h,
                    PriceChangePercentage24h = crypto.PriceChangePercentage24h,
                    PriceChangePercentage1h = crypto.PriceChangePercentage1h,
                    PriceChangePercentage7d = crypto.PriceChangePercentage7d,
                    MarketCapChange24h = crypto.MarketCapChange24h,
                    MarketCapChangePercentage24h = crypto.MarketCapChangePercentage24h,
                    CirculatingSupply = crypto.CirculatingSupply,
                    TotalSupply = crypto.TotalSupply,
                    MaxSupply = crypto.MaxSupply,
                    AllTimeHigh = crypto.AllTimeHigh,
                    AllTimeHighChangePercentage = crypto.AllTimeHighChangePercentage,
                    AllTimeHighDate = crypto.AllTimeHighDate,
                    AllTimeLow = crypto.AllTimeLow,
                    AllTimeLowChangePercentage = crypto.AllTimeLowChangePercentage,
                    AllTimeLowDate = crypto.AllTimeLowDate,
                    SparklineIn7d = crypto.SparklineIn7d,
                    LastUpdated = DateTime.UtcNow.AddHours(2)
                };
                _repository.AddCrypto(newCrypto);
            }
        }
        _repository.SaveChanges();
        if (cryptos.Count > 20)
        {
            await UpdateCryptoRankDatabase();
        }
    }

    public async Task UpdateCryptoRankDatabase()
    {
        
        var cryptos = _repository.GetAllCryptos().OrderByDescending(c => c.MarketCap);
        var rank = 1;
        foreach (var crypto in cryptos)
        {
            crypto.MarketCapRank = rank++;
            _repository.UpdateCrypto(crypto);
        }
        _repository.SaveChanges();
    }

    public Crypto RegisterCrypto(CryptoCreateUpdateDto dto)
    {
        var registeredCrypto = _repository.GetAllCryptos().FirstOrDefault(c => c.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));
        if (registeredCrypto != null)
        {
            throw new Exception("El nombre de la criptomoneda ya existe.");
        }

        Crypto crypto = new Crypto
        {
            Id = dto.Id,
            Name = dto.Name,
            Symbol = dto.Symbol,
            Image = dto.Image,
            Price = dto.Price,
            MarketCap = dto.MarketCap,
            MarketCapRank = null,
            FullyDilutedValuation = dto.FullyDilutedValuation,
            TotalVolume = dto.TotalVolume,
            High24h = dto.High24h,
            Low24h = dto.Low24h,
            PriceChange24h = dto.PriceChange24h,
            PriceChangePercentage24h = dto.PriceChangePercentage24h,
            PriceChangePercentage1h = dto.PriceChangePercentage1h,
            PriceChangePercentage7d = dto.PriceChangePercentage7d,
            MarketCapChange24h = dto.MarketCapChange24h,
            MarketCapChangePercentage24h = dto.MarketCapChangePercentage24h,
            CirculatingSupply = dto.CirculatingSupply,
            TotalSupply = dto.TotalSupply,
            MaxSupply = dto.MaxSupply,
            AllTimeHigh = dto.AllTimeHigh,
            AllTimeHighChangePercentage = dto.AllTimeHighChangePercentage,
            AllTimeHighDate = dto.AllTimeHighDate,
            AllTimeLow = dto.AllTimeLow,
            AllTimeLowChangePercentage = dto.AllTimeLowChangePercentage,
            AllTimeLowDate = dto.AllTimeLowDate,
            SparklineIn7d = dto.SparklineIn7d,
            LastUpdated = DateTime.UtcNow.AddHours(2)
        };
        _repository.AddCrypto(crypto);
        return crypto;
    }

    public IEnumerable<Crypto> GetAllCryptos(CryptoQueryParameters dto)
    {
        return _repository.GetAllCryptos(dto);
    }

    public Crypto GetCryptoById(string cryptoId)
    {
        var crypto = _repository.GetCrypto(cryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoId} no encontrada");
        }
        return crypto;
    }

    public void UpdateCrypto(string cryptoId, CryptoCreateUpdateDto dto)
    {
        var crypto = _repository.GetCrypto(cryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoId} no encontrada");
        }

        var registeredCrypto = _repository.GetAllCryptos().FirstOrDefault(c => c.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));
        if ((registeredCrypto != null) && (cryptoId != registeredCrypto.Id))
        {
            throw new Exception("El nombre de la criptomoneda ya existe.");
        }

        crypto.Id = dto.Id;
        crypto.Name = dto.Name;
        crypto.Symbol = dto.Symbol;
        crypto.Image = dto.Image;
        crypto.Price = dto.Price;
        crypto.MarketCap = dto.MarketCap;
        crypto.MarketCapRank = null;
        crypto.FullyDilutedValuation = dto.FullyDilutedValuation;
        crypto.TotalVolume = dto.TotalVolume;
        crypto.High24h = dto.High24h;
        crypto.Low24h = dto.Low24h;
        crypto.PriceChange24h = dto.PriceChange24h;
        crypto.PriceChangePercentage24h = dto.PriceChangePercentage24h;
        crypto.PriceChangePercentage1h = dto.PriceChangePercentage1h;
        crypto.PriceChangePercentage7d = dto.PriceChangePercentage7d;
        crypto.MarketCapChange24h = dto.MarketCapChange24h;
        crypto.MarketCapChangePercentage24h = dto.MarketCapChangePercentage24h;
        crypto.CirculatingSupply = dto.CirculatingSupply;
        crypto.TotalSupply = dto.TotalSupply;
        crypto.MaxSupply = dto.MaxSupply;
        crypto.AllTimeHigh = dto.AllTimeHigh;
        crypto.AllTimeHighChangePercentage = dto.AllTimeHighChangePercentage;
        crypto.AllTimeHighDate = dto.AllTimeHighDate;
        crypto.AllTimeLow = dto.AllTimeLow;
        crypto.AllTimeLowChangePercentage = dto.AllTimeLowChangePercentage;
        crypto.AllTimeLowDate = dto.AllTimeLowDate;
        crypto.SparklineIn7d = dto.SparklineIn7d;
        crypto.LastUpdated = DateTime.UtcNow.AddHours(2);   
        _repository.UpdateCrypto(crypto);
    }

    public void DeleteCrypto(string cryptoId)
    {
        var crypto = _repository.GetCrypto(cryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoId} no encontrada");
        }
        _repository.DeleteCrypto(cryptoId);
    }

    public List<Crypto> SearchCrypto(string query)
    {
        var cryptos = _repository.GetAllCryptos()
                                 .Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                             c.Symbol.Contains(query, StringComparison.OrdinalIgnoreCase))
                                             .OrderByDescending(c => c.MarketCap)
                                             .ToList();
        if (!cryptos.Any())
        {
            throw new KeyNotFoundException($"No se encontraron criptomonedas que coincidan con la búsqueda: {query}");
        }
        return cryptos;
    }
    
}