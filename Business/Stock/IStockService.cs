using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IStockService
{
    public Task UpdateStocksDatabase(List<Stock> stocks);
    public Stock RegisterStock(StockCreateUpdateDto dto);
    public IEnumerable<Stock> GetAllStocks(StockQueryParameters? dto = null);
    public Stock GetStockById(string stockId);
    public void UpdateStock(string stockId, StockCreateUpdateDto dto);
    public void DeleteStock(string stockId);
}
