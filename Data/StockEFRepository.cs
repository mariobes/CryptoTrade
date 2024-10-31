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

        public IEnumerable<Stock> GetAllStocks() 
        {
            var stock = _context.Stocks;
            return stock;
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