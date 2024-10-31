using CryptoTrade.Data;
using CryptoTrade.Models;

namespace CryptoTrade.Business;

public class StockService : IStockService
{
    private readonly IStockRepository _repository;

    public StockService(IStockRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Stock> GetAllStocks()
    {
        return _repository.GetAllStocks();
    }

    public Stock GetStockById(int stockId)
    {
        var stock = _repository.GetStock(stockId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {stockId} no encontrada");
        }
        return stock;
    }
    
}