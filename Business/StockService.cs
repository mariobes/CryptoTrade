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

    public Stock RegisterStock(StockCreateUpdateDTO dto)
    {
        var registeredStock = _repository.GetAllStocks().FirstOrDefault(s => s.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));
        if (registeredStock != null)
        {
            throw new Exception("El nombre de la acción ya existe.");
        }
        
        Stock stock = new Stock
        {
            Name = dto.Name,
            Value = dto.Value,
            Description = dto.Description,
            Website = dto.Website,
            CompanyValue = dto.CompanyValue,
            EarningPerShare = dto.EarningPerShare,
            Category = dto.Category,
            DividendYield = dto.DividendYield
        };
        _repository.AddStock(stock);
        return stock;
    }

    public IEnumerable<Stock> GetAllStocks(StockQueryParameters dto)
    {
        return _repository.GetAllStocks(dto);
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

    public void UpdateStock(int stockId, StockCreateUpdateDTO dto)
    {
        var stock = _repository.GetStock(stockId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {stockId} no encontrada");
        }

        var registeredStock = _repository.GetAllStocks().FirstOrDefault(s => s.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase));
        if ((registeredStock != null) && (stockId != registeredStock.Id))
        {
            throw new Exception("El nombre de la acción ya existe.");
        }

        stock.Name = dto.Name;
        stock.Value = dto.Value;
        stock.Description = dto.Description;
        stock.Website = dto.Website;
        stock.CompanyValue = dto.CompanyValue;
        stock.EarningPerShare = dto.EarningPerShare;
        stock.Category = dto.Category;
        stock.DividendYield = dto.DividendYield;
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