using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IStockService
{
    public Stock RegisterStock(StockCreateUpdateDTO stockCreateUpdateDTO);
    public IEnumerable<Stock> GetAllStocks();
    public Stock GetStockById(int stockId);
    public void UpdateStock(int stockId, StockCreateUpdateDTO stockCreateUpdateDTO);
    public void DeleteStock(int stockId);
}
