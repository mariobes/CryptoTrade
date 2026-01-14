using CryptoTrade.Models;
using System.Text.Json;

namespace CryptoTrade.Data;

public class CryptoJsonRepository : ICryptoRepository
{
    private Dictionary<string, Crypto> _cryptos = new Dictionary<string, Crypto>();
    private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Data", "JsonData", "Cryptos.json");

    public CryptoJsonRepository()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var cryptos = JsonSerializer.Deserialize<IEnumerable<Crypto>>(jsonString);
                _cryptos = cryptos.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the crypto file", e);
            }
        }
    }

    public void AddCrypto(Crypto crypto)
    {
        _cryptos[crypto.Id] = crypto;
        SaveChanges();
    }

    public IEnumerable<Crypto> GetAllCryptos(CryptoQueryParameters dto) 
    {
        var query = _cryptos.Values.AsQueryable();

        if (dto != null)
        {
            query = dto.SortBy switch
            {
                EnumCryptoSortOptions.marketCapRank => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(c => c.MarketCapRank)
                    : query.OrderByDescending(c => c.MarketCapRank),
                EnumCryptoSortOptions.name => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(c => c.Name)
                    : query.OrderByDescending(c => c.Name),
                EnumCryptoSortOptions.price => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(c => c.Price)
                    : query.OrderByDescending(c => c.Price),
                EnumCryptoSortOptions.priceChangePercentage1h => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(c => c.PriceChangePercentage1h)
                    : query.OrderByDescending(c => c.PriceChangePercentage1h),
                EnumCryptoSortOptions.priceChangePercentage24h => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(c => c.PriceChangePercentage24h)
                    : query.OrderByDescending(c => c.PriceChangePercentage24h),
                EnumCryptoSortOptions.priceChangePercentage7d => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(c => c.PriceChangePercentage7d)
                    : query.OrderByDescending(c => c.PriceChangePercentage7d),
                EnumCryptoSortOptions.totalVolume => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(c => c.TotalVolume)
                    : query.OrderByDescending(c => c.TotalVolume),
                EnumCryptoSortOptions.circulatingSupply => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(c => c.CirculatingSupply)
                    : query.OrderByDescending(c => c.CirculatingSupply),

                _ => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(c => c.MarketCap)
                    : query.OrderByDescending(c => c.MarketCap),
            };
        }

        var result = query.ToList();
        return result;
    }

    public Crypto GetCrypto(string cryptoId) => _cryptos.FirstOrDefault(c => c.Value.Id == cryptoId).Value;

    public void UpdateCrypto(Crypto crypto)
    {
        _cryptos[crypto.Id] = crypto;
        SaveChanges();
    }

    public void DeleteCrypto(string cryptoId)
    {
        _cryptos.Remove(cryptoId);
        SaveChanges();
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_cryptos.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the crypto file", e);
        }
    }
}