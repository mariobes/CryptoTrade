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

    public async Task UpdateStocksDatabase(List<Stock> stocks)
    {
        foreach (var stock in stocks)
        {
            var registeredStock = _repository.GetAllStocks().FirstOrDefault(s => s.Symbol.Equals(stock.Symbol, StringComparison.OrdinalIgnoreCase));
            if (registeredStock != null)
            {
                registeredStock.Price = stock.Price;
                registeredStock.MarketCap = stock.MarketCap;
                registeredStock.LastAnnualDividend = stock.LastAnnualDividend;
                registeredStock.Volume = stock.Volume;
                registeredStock.Changes = stock.Changes;
                registeredStock.LastUpdated = DateTime.Now;
                _repository.UpdateStock(registeredStock);
            }
            else
            {
                var newStock = new Stock
                {
                    Id = stock.Symbol.ToLower(),
                    Name = stock.Name,
                    Symbol = stock.Symbol,
                    Price = stock.Price,
                    MarketCap = stock.MarketCap,
                    Sector = stock.Sector,
                    Industry = stock.Industry,
                    LastAnnualDividend = stock.LastAnnualDividend,
                    Volume = stock.Volume,
                    Exchange = stock.Exchange,
                    ExchangeShortName = stock.ExchangeShortName,
                    Country = stock.Country,
                    Changes = stock.Changes,
                    Currency = stock.Currency,
                    Isin = stock.Isin,
                    Website = stock.Website,
                    Description = stock.Description,
                    Ceo = stock.Ceo,
                    Image = stock.Image,
                    LastUpdated = DateTime.Now
                };
                _repository.AddStock(newStock);
            }
        }
        _repository.SaveChanges();
    }

    public Stock RegisterStock(StockCreateUpdateDTO dto)
    {
        var registeredStock = _repository.GetAllStocks().FirstOrDefault(s => s.Symbol.Equals(dto.Symbol, StringComparison.OrdinalIgnoreCase));
        if (registeredStock != null)
        {
            throw new Exception("El símbolo de la acción ya existe.");
        }
        
        Stock stock = new Stock
        {
            Id = dto.Symbol.ToLower(),
            Name = dto.Name,
            Symbol = dto.Symbol,
            Price = dto.Price,
            MarketCap = dto.MarketCap,
            Sector = dto.Sector,
            Industry = dto.Industry,
            LastAnnualDividend = dto.LastAnnualDividend,
            Volume = dto.Volume,
            Exchange = dto.Exchange,
            ExchangeShortName = dto.ExchangeShortName,
            Country = dto.Country,
            Changes = dto.Changes,
            Currency = dto.Currency,
            Isin = dto.Isin,
            Website = dto.Website,
            Description = dto.Description,
            Ceo = dto.Ceo,
            Image = dto.Image,
            LastUpdated = DateTime.Now
        };
        _repository.AddStock(stock);
        return stock;
    }

    public IEnumerable<Stock> GetAllStocks(StockQueryParameters dto)
    {
        return _repository.GetAllStocks(dto);
    }

    public Stock GetStockById(string stockId)
    {
        var stock = _repository.GetStock(stockId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {stockId} no encontrada");
        }
        return stock;
    }

    public void UpdateStock(string stockId, StockCreateUpdateDTO dto)
    {
        var stock = _repository.GetStock(stockId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {stockId} no encontrada");
        }

        var registeredStock = _repository.GetAllStocks().FirstOrDefault(s => s.Symbol.Equals(dto.Symbol, StringComparison.OrdinalIgnoreCase));
        if ((registeredStock != null) && (stockId != registeredStock.Id))
        {
            throw new Exception("El sñimbolo de la acción ya existe.");
        }

        stock.Id = dto.Symbol.ToLower();
        stock.Name = dto.Name;
        stock.Symbol = dto.Symbol;
        stock.Price = dto.Price;
        stock.MarketCap = dto.MarketCap;
        stock.Sector = dto.Sector;
        stock.Industry = dto.Industry;
        stock.LastAnnualDividend = dto.LastAnnualDividend;
        stock.Volume = dto.Volume;
        stock.Exchange = dto.Exchange;
        stock.ExchangeShortName = dto.ExchangeShortName;
        stock.Country = dto.Country;
        stock.Changes = dto.Changes;
        stock.Currency = dto.Currency;
        stock.Isin = dto.Isin;
        stock.Website = dto.Website;
        stock.Description = dto.Description;
        stock.Ceo = dto.Ceo;
        stock.Image = dto.Image;
        stock.LastUpdated = DateTime.Now;
        _repository.UpdateStock(stock);
    }

    public void DeleteStock(string stockId)
    {
        var stock = _repository.GetStock(stockId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {stockId} no encontrada");
        }
        _repository.DeleteStock(stockId);
    }
    
}