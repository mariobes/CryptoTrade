using CryptoTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Data
{
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
                    EnumSortOptions.name => dto.Order == EnumOrderOptions.asc
                        ? query.OrderBy(s => s.Name)
                        : query.OrderByDescending(s => s.Name),

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

        public void DeleteStock(string stockId) {
            var stock = GetStock(stockId);
            _context.Stocks.Remove(stock);
            SaveChanges();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
 
    }   
}