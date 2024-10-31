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

    public Stock RegisterStock(StockCreateUpdateDTO stockCreateUpdateDTO)
    {
        var registeredStock = _repository.GetAllStocks().FirstOrDefault(s => s.Name.Equals(stockCreateUpdateDTO.Name, StringComparison.OrdinalIgnoreCase));
        if (registeredStock != null)
        {
            throw new Exception("El nombre de la acción ya existe.");
        }
        Stock stock = new(stockCreateUpdateDTO.Name, stockCreateUpdateDTO.Value, stockCreateUpdateDTO.Description, stockCreateUpdateDTO.Ranking, stockCreateUpdateDTO.Website, stockCreateUpdateDTO.CompanyValue, stockCreateUpdateDTO.EarningPerShare, stockCreateUpdateDTO.Category, stockCreateUpdateDTO.DividendYield);
        _repository.AddStock(stock);
        return stock;
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

    public void UpdateStock(int stockId, StockCreateUpdateDTO stockCreateUpdateDTO)
    {
        var stock = _repository.GetStock(stockId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {stockId} no encontrada");
        }

        var registeredStock = _repository.GetAllStocks().FirstOrDefault(s => s.Name.Equals(stockCreateUpdateDTO.Name, StringComparison.OrdinalIgnoreCase));
        if ((registeredStock != null) && (stockId != registeredStock.Id))
        {
            throw new Exception("El nombre de la acción ya existe.");
        }

        stock.Name = stockCreateUpdateDTO.Name;
        stock.Value = stockCreateUpdateDTO.Value;
        stock.Description = stockCreateUpdateDTO.Description;
        stock.Ranking = stockCreateUpdateDTO.Ranking;
        stock.Website = stockCreateUpdateDTO.Website;
        stock.CompanyValue = stockCreateUpdateDTO.CompanyValue;
        stock.EarningPerShare = stockCreateUpdateDTO.EarningPerShare;
        stock.Category = stockCreateUpdateDTO.Category;
        stock.DividendYield = stockCreateUpdateDTO.DividendYield;
        _repository.UpdateStock(stock);
    }

    public void DeleteStock(int stockId)
    {
        var stock = _repository.GetStock(stockId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {stockId} no encontrada");
        }
        _repository.DeleteStock(stockId);
    }
    
}