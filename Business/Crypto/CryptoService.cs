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
                registeredCrypto.FullyDilutedValuation = crypto.FullyDilutedValuation;
                registeredCrypto.TotalVolume = crypto.TotalVolume;
                registeredCrypto.High24h = crypto.High24h;
                registeredCrypto.Low24h = crypto.Low24h;
                registeredCrypto.PriceChange24h = crypto.PriceChange24h;
                registeredCrypto.PriceChangePercentage24h = crypto.PriceChangePercentage24h;
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
                registeredCrypto.LastUpdated = DateTime.Now;
                _repository.UpdateCrypto(registeredCrypto);
            }
            else
            {
                var newCrypto = new Crypto
                {
                    Id = crypto.Name.ToLower().Replace(" ", "-"),
                    Name = crypto.Name,
                    Symbol = crypto.Symbol,
                    Image = crypto.Image,
                    Price = crypto.Price,
                    MarketCap = crypto.MarketCap,
                    FullyDilutedValuation = crypto.FullyDilutedValuation,
                    TotalVolume = crypto.TotalVolume,
                    High24h = crypto.High24h,
                    Low24h = crypto.Low24h,
                    PriceChange24h = crypto.PriceChange24h,
                    PriceChangePercentage24h = crypto.PriceChangePercentage24h,
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
                    LastUpdated = DateTime.Now
                };
                _repository.AddCrypto(newCrypto);
            }
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
            Id = dto.Name.ToLower().Replace(" ", "-"),
            Name = dto.Name,
            Symbol = dto.Symbol,
            Image = dto.Image,
            Price = dto.Price,
            MarketCap = dto.MarketCap,
            FullyDilutedValuation = dto.FullyDilutedValuation,
            TotalVolume = dto.TotalVolume,
            High24h = dto.High24h,
            Low24h = dto.Low24h,
            PriceChange24h = dto.PriceChange24h,
            PriceChangePercentage24h = dto.PriceChangePercentage24h,
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
            LastUpdated = DateTime.Now
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

        crypto.Id = dto.Name.ToLower().Replace(" ", "-");
        crypto.Name = dto.Name;
        crypto.Symbol = dto.Symbol;
        crypto.Image = dto.Image;
        crypto.Price = dto.Price;
        crypto.MarketCap = dto.MarketCap;
        crypto.FullyDilutedValuation = dto.FullyDilutedValuation;
        crypto.TotalVolume = dto.TotalVolume;
        crypto.High24h = dto.High24h;
        crypto.Low24h = dto.Low24h;
        crypto.PriceChange24h = dto.PriceChange24h;
        crypto.PriceChangePercentage24h = dto.PriceChangePercentage24h;
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
        crypto.LastUpdated = DateTime.Now;       
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
    
}