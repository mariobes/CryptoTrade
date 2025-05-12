using CryptoTrade.Data;
using CryptoTrade.Models;

namespace CryptoTrade.Business;

public class MarketService : IMarketService
{
    private readonly IMarketRepository _repository;

    public MarketService(IMarketRepository repository)
    {
        _repository = repository;
    }

    public async Task DeleteCryptoIndexDatabase()
    {
        _repository.DeleteCryptoIndex();
    }

    public async Task UpdateCryptoIndexDatabase(CryptoIndexDto cryptoIndexDto)
    {
        var cryptoIndex = new CryptoIndex
        {
            Name = cryptoIndexDto.Name,
            Value = cryptoIndexDto.Value,
            ChangePercentage = cryptoIndexDto.ChangePercentage,
            Sentiment = cryptoIndexDto.Sentiment,
            LastUpdated = DateTime.UtcNow.AddHours(2)
        };
        _repository.AddCryptoIndex(cryptoIndex);
        _repository.SaveChanges();
    }

    public IEnumerable<CryptoIndex> GetCryptoIndices()
    {
        return _repository.GetCryptoIndices();
    }

    public async Task UpdateCryptoTrendingDatabase(List<AssetMarketDto> cryptosTrending)
    {
        _repository.DeleteCryptoTrending();

        foreach (var crypto in cryptosTrending)
        {
            var cryptoTrending = new CryptoTrending
            {
                Id = crypto.Id,
                Name = crypto.Name,
                Symbol = crypto.Symbol,
                Image = crypto.Image,
                Price = crypto.Price,
                ChangePercentage = crypto.ChangePercentage,
                LastUpdated = DateTime.UtcNow.AddHours(2)
            };
            _repository.AddCryptoTrending(cryptoTrending);
        }
        _repository.SaveChanges();
    }

    public IEnumerable<CryptoTrending> GetCryptosTrending()
    {
        return _repository.GetCryptosTrending();
    }

    public async Task UpdateStockTrendingDatabase(List<AssetMarketDto> stocksTrending)
    {
        _repository.DeleteStockTrending();

        foreach (var stock in stocksTrending)
        {
            var stockTrending = new StockTrending
            {
                Id = stock.Id.ToLower(),
                Name = stock.Name,
                Symbol = stock.Symbol,
                Image = stock.Image,
                Price = stock.Price,
                ChangePercentage = stock.ChangePercentage,
                LastUpdated = DateTime.UtcNow.AddHours(2)
            };
            _repository.AddStockTrending(stockTrending);
        }
        _repository.SaveChanges();
    }

    public IEnumerable<StockTrending> GetStocksTrending()
    {
        return _repository.GetStocksTrending();
    }

    public async Task UpdateStockGainerDatabase(List<AssetMarketDto> stocksGainer)
    {
        _repository.DeleteStockGainer();

        foreach (var stock in stocksGainer)
        {
            var stockGainer = new StockGainer
            {
                Id = stock.Id.ToLower(),
                Name = stock.Name,
                Symbol = stock.Symbol,
                Image = stock.Image,
                Price = stock.Price,
                ChangePercentage = stock.ChangePercentage,
                LastUpdated = DateTime.UtcNow.AddHours(2)
            };
            _repository.AddStockGainer(stockGainer);
        }
        _repository.SaveChanges();
    }

    public IEnumerable<StockGainer> GetStocksGainers()
    {
        return _repository.GetStocksGainers();
    }

    public async Task UpdateStockLoserDatabase(List<AssetMarketDto> stocksLoser)
    {
        _repository.DeleteStockLoser();

        foreach (var stock in stocksLoser)
        {
            var stockLoser = new StockLoser
            {
                Id = stock.Id.ToLower(),
                Name = stock.Name,
                Symbol = stock.Symbol,
                Image = stock.Image,
                Price = stock.Price,
                ChangePercentage = stock.ChangePercentage,
                LastUpdated = DateTime.UtcNow.AddHours(2)
            };
            _repository.AddStockLoser(stockLoser);
        }
        _repository.SaveChanges();
    }

    public IEnumerable<StockLoser> GetStocksLosers()
    {
        return _repository.GetStocksLosers();
    }

    public async Task UpdateStockMostActiveDatabase(List<AssetMarketDto> stocksMostActive)
    {
        _repository.DeleteStockMostActive();

        foreach (var stock in stocksMostActive)
        {
            var stockMostActive = new StockMostActive
            {
                Id = stock.Id.ToLower(),
                Name = stock.Name,
                Symbol = stock.Symbol,
                Image = stock.Image,
                Price = stock.Price,
                ChangePercentage = stock.ChangePercentage,
                LastUpdated = DateTime.UtcNow.AddHours(2)
            };
            _repository.AddStockMostActive(stockMostActive);
        }
        _repository.SaveChanges();
    }

    public IEnumerable<StockMostActive> GetStocksMostActives()
    {
        return _repository.GetStocksMostActives();
    }
}