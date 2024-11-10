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

        public IEnumerable<Stock> GetAllStocks(StockQueryParameters stockQueryParameters) 
        {
            var query = _context.Stocks.AsQueryable();

            if (stockQueryParameters != null)
            {
                query = stockQueryParameters.SortBy switch
                {
                    EnumSortOptions.name => stockQueryParameters.Order == EnumOrderOptions.asc
                        ? query.OrderBy(s => s.Name)
                        : query.OrderByDescending(s => s.Name),

                    EnumSortOptions.marketCap => stockQueryParameters.Order == EnumOrderOptions.asc
                        ? query.OrderBy(s => s.CompanyValue)
                        : query.OrderByDescending(s => s.CompanyValue),

                    _ => stockQueryParameters.Order == EnumOrderOptions.asc
                        ? query.OrderBy(s => s.Value)
                        : query.OrderByDescending(s => s.Value),
                };
            }

            var result = query.ToList();
            return result;
        }

        public Stock GetStock(int stockId)
        {
            var stock = _context.Stocks.FirstOrDefault(stock => stock.Id == stockId);
            return stock;
        }

        public void UpdateStock(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteStock(int stockId) {
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