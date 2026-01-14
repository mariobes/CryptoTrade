using CryptoTrade.Models;

namespace CryptoTrade.Data;

public interface IStockRepository
{
    public void AddStock(Stock stock);
    public Stock GetStock(string stockId);
    IEnumerable<Stock> GetAllStocks(StockQueryParameters? dto = null);
    public void UpdateStock(Stock stock);
    public void DeleteStock(string stockId);
    void SaveChanges();
}