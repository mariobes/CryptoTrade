using CryptoTrade.Models;
using System.Text.Json;

namespace CryptoTrade.Data;

public class StockJsonRepository : IStockRepository
{
    private Dictionary<string, Stock> _stocks = new Dictionary<string, Stock>();
    private readonly string _filePath;

    public StockJsonRepository()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        _filePath = Path.Combine(basePath, "JsonData", "Stocks.json");

        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var stocks = JsonSerializer.Deserialize<IEnumerable<Stock>>(jsonString);
                _stocks = stocks.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the stock file", e);
            }
        }
    }

    public void AddStock(Stock stock)
    {
        _stocks[stock.Id] = stock;
        SaveChanges();
    }

    public IEnumerable<Stock> GetAllStocks(StockQueryParameters dto) 
    {
        var query = _stocks.Values.AsQueryable();

        if (dto != null)
        {
            query = dto.SortBy switch
            {
                EnumStockSortOptions.marketCapRank => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.MarketCapRank)
                    : query.OrderByDescending(s => s.MarketCapRank),
                EnumStockSortOptions.name => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Name)
                    : query.OrderByDescending(s => s.Name),
                EnumStockSortOptions.price => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Price)
                    : query.OrderByDescending(s => s.Price),
                EnumStockSortOptions.priceChange24h => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Changes)
                    : query.OrderByDescending(s => s.Changes),
                EnumStockSortOptions.priceChangePercentage24h => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.ChangesPercentage)
                    : query.OrderByDescending(s => s.ChangesPercentage),
                EnumStockSortOptions.currency => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Currency)
                    : query.OrderByDescending(s => s.Currency),
                EnumStockSortOptions.lastDividend => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.LastAnnualDividend)
                    : query.OrderByDescending(s => s.LastAnnualDividend),
                EnumStockSortOptions.averageVolume => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Volume)
                    : query.OrderByDescending(s => s.Volume),

                _ => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.MarketCap)
                    : query.OrderByDescending(s => s.MarketCap),
            };
        }

        var result = query.ToList();
        return result;
    }

    public Stock GetStock(string stockId) => _stocks.FirstOrDefault(s => s.Value.Id == stockId).Value;

    public void UpdateStock(Stock stock)
    {
        _stocks[stock.Id] = stock;
        SaveChanges();
    }

    public void DeleteStock(string stockId)
    {
        _stocks.Remove(stockId);
        SaveChanges();
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_stocks.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the stock file", e);
        }
    }
}