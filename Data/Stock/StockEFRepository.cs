using CryptoTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Data;

public class StockEFRepository : IStockRepository
{
    private readonly CryptoTradeContext _context;

    public StockEFRepository(CryptoTradeContext context)
    {
        _context = context;
    }

    public void AddStock(Stock stock)
    {
        _context.Stocks.Add(stock);
        SaveChanges();
    }

    public IEnumerable<Stock> GetAllStocks(StockQueryParameters dto) 
    {
        var query = _context.Stocks.AsQueryable();

        if (dto != null)
        {
            query = dto.SortBy switch
            {
                EnumStockSortOptions.marketCapRank => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.MarketCapRank)
                    : query.OrderByDescending(s => s.MarketCapRank),
                EnumStockSortOptions.name => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Name)
                    : query.OrderByDescending(s => s.Name),
                EnumStockSortOptions.price => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Price)
                    : query.OrderByDescending(s => s.Price),
                EnumStockSortOptions.priceChange24h => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Changes)
                    : query.OrderByDescending(s => s.Changes),
                EnumStockSortOptions.priceChangePercentage24h => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.ChangesPercentage)
                    : query.OrderByDescending(s => s.ChangesPercentage),
                EnumStockSortOptions.currency => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Currency)
                    : query.OrderByDescending(s => s.Currency),
                EnumStockSortOptions.lastDividend => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.LastAnnualDividend)
                    : query.OrderByDescending(s => s.LastAnnualDividend),
                EnumStockSortOptions.averageVolume => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.Volume)
                    : query.OrderByDescending(s => s.Volume),

                _ => dto.Order == EnumOrderOptions.asc
                    ? query.OrderBy(s => s.MarketCap)
                    : query.OrderByDescending(s => s.MarketCap),
            };
        }

        var result = query.ToList();
        return result;
    }

    public Stock GetStock(string stockId)
    {
        var stock = _context.Stocks.FirstOrDefault(s => s.Id == stockId);
        return stock;
    }

    public void UpdateStock(Stock stock)
    {
        _context.Entry(stock).State = EntityState.Modified;
        SaveChanges();
    }

    public void DeleteStock(string stockId) 
    {
        var stock = GetStock(stockId);
        _context.Stocks.Remove(stock);
        SaveChanges();
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}