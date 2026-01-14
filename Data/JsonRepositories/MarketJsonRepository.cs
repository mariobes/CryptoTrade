using CryptoTrade.Models;
using System.Text.Json;

namespace CryptoTrade.Data;

public class MarketJsonRepository : IMarketRepository
{
    private Dictionary<string, CryptoIndex> _cryptoIndices = new Dictionary<string, CryptoIndex>();
    private Dictionary<string, CryptoTrending> _cryptoTrendings = new Dictionary<string, CryptoTrending>();
    private Dictionary<string, StockTrending> _stockTrendings = new Dictionary<string, StockTrending>();
    private Dictionary<string, StockGainer> _stockGainers = new Dictionary<string, StockGainer>();
    private Dictionary<string, StockLoser> _stockLosers = new Dictionary<string, StockLoser>();
    private Dictionary<string, StockMostActive> _stockMostActives = new Dictionary<string, StockMostActive>();

    private readonly string _cryptoIndexPath;
    private readonly string _cryptoTrendingPath;
    private readonly string _stockTrendingPath;
    private readonly string _stockGainerPath;
    private readonly string _stockLoserPath;
    private readonly string _stockMostActivePath;

    private static int CryptoIndexIdSeed { get; set; }

    public MarketJsonRepository()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        _cryptoIndexPath = Path.Combine(basePath, "JsonData", "CryptoIndices.json");
        _cryptoTrendingPath = Path.Combine(basePath, "JsonData", "CryptoTrendings.json");
        _stockTrendingPath = Path.Combine(basePath, "JsonData", "StockTrendings.json");
        _stockGainerPath = Path.Combine(basePath, "JsonData", "StockGainers.json");
        _stockLoserPath = Path.Combine(basePath, "JsonData", "StockLosers.json");
        _stockMostActivePath = Path.Combine(basePath, "JsonData", "StockMostActives.json");

        // CRYPTO INDEX
        if (File.Exists(_cryptoIndexPath))
        {
            try
            {
                string jsonString = File.ReadAllText(_cryptoIndexPath);
                var cryptoIndices = JsonSerializer.Deserialize<IEnumerable<CryptoIndex>>(jsonString);
                _cryptoIndices = cryptoIndices.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the crypto index file", e);
            }
        }

        // CRYPTO TRENDING
        if (File.Exists(_cryptoTrendingPath))
        {
            try
            {
                string jsonString = File.ReadAllText(_cryptoTrendingPath);
                var cryptoTrendings = JsonSerializer.Deserialize<IEnumerable<CryptoTrending>>(jsonString);
                _cryptoTrendings = cryptoTrendings.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the crypto trending file", e);
            }
        }

        // STOCK TRENDING
        if (File.Exists(_stockTrendingPath))
        {
            try
            {
                string jsonString = File.ReadAllText(_stockTrendingPath);
                var stockTrendings = JsonSerializer.Deserialize<IEnumerable<StockTrending>>(jsonString);
                _stockTrendings = stockTrendings.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the stock trending file", e);
            }
        }

        // STOCK GAINER
        if (File.Exists(_stockGainerPath))
        {
            try
            {
                string jsonString = File.ReadAllText(_stockGainerPath);
                var stockGainers = JsonSerializer.Deserialize<IEnumerable<StockGainer>>(jsonString);
                _stockGainers = stockGainers.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the stock gainer file", e);
            }
        }

        // STOCK LOSER
        if (File.Exists(_stockLoserPath))
        {
            try
            {
                string jsonString = File.ReadAllText(_stockLoserPath);
                var stockLosers = JsonSerializer.Deserialize<IEnumerable<StockLoser>>(jsonString);
                _stockLosers = stockLosers.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the stock loser file", e);
            }
        }

        // STOCK MOST ACTIVE
        if (File.Exists(_stockMostActivePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_stockMostActivePath);
                var stockMostActives = JsonSerializer.Deserialize<IEnumerable<StockMostActive>>(jsonString);
                _stockMostActives = stockMostActives.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the stock most active file", e);
            }
        }

        CryptoIndexIdSeed = _cryptoIndices.Any() ? _cryptoIndices.Values.Max(u => u.Id) + 1 : 1;
    }

    // CRYPTO INDEX
    public void AddCryptoIndex(CryptoIndex cryptoIndex)
    {
        cryptoIndex.Id = CryptoIndexIdSeed++;
        _cryptoIndices[cryptoIndex.Id.ToString()] = cryptoIndex;
        SaveChanges();
    }

    public void DeleteCryptoIndex()
    {
        _cryptoIndices.Clear();
        SaveChanges(); 
    }

    public IEnumerable<CryptoIndex> GetCryptoIndices() 
    {
        return _cryptoIndices.Values;
    }

    // CRYPTO TRENDING
    public void AddCryptoTrending(CryptoTrending cryptoTrending)
    {
        _cryptoTrendings[cryptoTrending.Id] = cryptoTrending;
        SaveChanges();
    }

    public void DeleteCryptoTrending()
    {
        _cryptoTrendings.Clear();
        SaveChanges(); 
    }

    public IEnumerable<CryptoTrending> GetCryptosTrending() 
    {
        return _cryptoTrendings.Values;
    }

    // STOCK TRENDING
    public void AddStockTrending(StockTrending stockTrending)
    {
        _stockTrendings[stockTrending.Id] = stockTrending;
        SaveChanges();
    }

    public void DeleteStockTrending()
    {
        _stockTrendings.Clear();
        SaveChanges(); 
    }

    public IEnumerable<StockTrending> GetStocksTrending() 
    {
        return _stockTrendings.Values;
    }

    // STOCK GAINER
    public void AddStockGainer(StockGainer stockGainer)
    {
        _stockGainers[stockGainer.Id] = stockGainer;
        SaveChanges();
    }

    public void DeleteStockGainer()
    {
        _stockGainers.Clear();
        SaveChanges(); 
    }

    public IEnumerable<StockGainer> GetStocksGainers() 
    {
        return _stockGainers.Values;
    }

    // STOCK LOSER
    public void AddStockLoser(StockLoser stockLoser)
    {
        _stockLosers[stockLoser.Id] = stockLoser;
        SaveChanges();
    }
    public void DeleteStockLoser()
    {
        _stockLosers.Clear();
        SaveChanges(); 
    }

    public IEnumerable<StockLoser> GetStocksLosers() 
    {
        return _stockLosers.Values;
    }

    // STOCK MOST ACTIVE
    public void AddStockMostActive(StockMostActive stockMostActive)
    {
        _stockMostActives[stockMostActive.Id] = stockMostActive;
        SaveChanges();
    }

    public void DeleteStockMostActive()
    {
        _stockMostActives.Clear();
        SaveChanges(); 
    }

    public IEnumerable<StockMostActive> GetStocksMostActives() 
    {
        return _stockMostActives.Values;
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            File.WriteAllText(_cryptoIndexPath,
                JsonSerializer.Serialize(_cryptoIndices.Values, options));

            File.WriteAllText(_cryptoTrendingPath,
                JsonSerializer.Serialize(_cryptoTrendings.Values, options));

            File.WriteAllText(_stockTrendingPath,
                JsonSerializer.Serialize(_stockTrendings.Values, options));

            File.WriteAllText(_stockGainerPath,
                JsonSerializer.Serialize(_stockGainers.Values, options));

            File.WriteAllText(_stockLoserPath,
                JsonSerializer.Serialize(_stockLosers.Values, options));

            File.WriteAllText(_stockMostActivePath,
                JsonSerializer.Serialize(_stockMostActives.Values, options));
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the file", e);
        }
    }
}