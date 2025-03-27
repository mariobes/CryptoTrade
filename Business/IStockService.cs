using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IStockService
{
    public Stock RegisterStock(StockCreateUpdateDTO dto);
    public IEnumerable<Stock> GetAllStocks(StockQueryParameters? dto = null);
    public Stock GetStockById(int stockId);
    public void UpdateStock(int stockId, StockCreateUpdateDTO dto);
    public void DeleteStock(int stockId);
}
