using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IStockService
{
    public IEnumerable<Stock> GetAllStocks();
}
