using CryptoTrade.Models;

namespace CryptoTrade.Data;

public interface IStockRepository
{
    public void AddStock(Stock stock);
    public Stock GetStock(int stockId);
    IEnumerable<Stock> GetAllStocks(StockQueryParameters? dto = null);
    public void UpdateStock(Stock stock);
    public void DeleteStock(int stockId);
    void SaveChanges();
}
