using CryptoTrade.Models;

namespace CryptoTrade.Data;

public class MarketEFRepository : IMarketRepository
{
    private readonly CryptoTradeContext _context;

    public MarketEFRepository(CryptoTradeContext context)
    {
        _context = context;
    }

    public void AddCryptoIndex(CryptoIndex cryptoIndex)
    {
        _context.CryptoIndices.Add(cryptoIndex);
        SaveChanges();
    }

    public void DeleteCryptoIndex() 
    {
        var cryptosIndex = _context.CryptoIndices.ToList();
        _context.CryptoIndices.RemoveRange(cryptosIndex);
        SaveChanges();
    }

    public IEnumerable<CryptoIndex> GetCryptoIndices() 
    {
        var cryptoIndices = _context.CryptoIndices;
        return cryptoIndices;
    }

    public void AddCryptoTrending(CryptoTrending cryptoTrending)
    {
        _context.CryptoTrendings.Add(cryptoTrending);
        SaveChanges();
    }

    public void DeleteCryptoTrending() {
        var cryptosTrending = _context.CryptoTrendings.ToList();
        _context.CryptoTrendings.RemoveRange(cryptosTrending);
        SaveChanges();
    }

    public IEnumerable<CryptoTrending> GetCryptosTrending() 
    {
        var cryptosTrending = _context.CryptoTrendings;
        return cryptosTrending;
    }

    public void AddStockTrending(StockTrending stockTrending)
    {
        _context.StockTrendings.Add(stockTrending);
        SaveChanges();
    }

    public void DeleteStockTrending() {
        var stocksTrending = _context.StockTrendings.ToList();
        _context.StockTrendings.RemoveRange(stocksTrending);
        SaveChanges();
    }

    public IEnumerable<StockTrending> GetStocksTrending() 
    {
        var stocksTrending = _context.StockTrendings;
        return stocksTrending;
    }

    public void AddStockGainer(StockGainer stockGainer)
    {
        _context.StockGainers.Add(stockGainer);
        SaveChanges();
    }

    public void DeleteStockGainer() {
        var stocksGainer = _context.StockGainers.ToList();
        _context.StockGainers.RemoveRange(stocksGainer);
        SaveChanges();
    }

    public IEnumerable<StockGainer> GetStocksGainers() 
    {
        var stocksGainers = _context.StockGainers;
        return stocksGainers;
    }

    public void AddStockLoser(StockLoser stockLoser)
    {
        _context.StockLosers.Add(stockLoser);
        SaveChanges();
    }

    public void DeleteStockLoser() {
        var stocksLoser = _context.StockLosers.ToList();
        _context.StockLosers.RemoveRange(stocksLoser);
        SaveChanges();
    }

    public IEnumerable<StockLoser> GetStocksLosers() 
    {
        var stocksLosers = _context.StockLosers;
        return stocksLosers;
    }

    public void AddStockMostActive(StockMostActive stockMostActive)
    {
        _context.StockMostActives.Add(stockMostActive);
        SaveChanges();
    }

    public void DeleteStockMostActive() {
        var stocksMostActive = _context.StockMostActives.ToList();
        _context.StockMostActives.RemoveRange(stocksMostActive);
        SaveChanges();
    }

    public IEnumerable<StockMostActive> GetStocksMostActives() 
    {
        var stocksMostActives = _context.StockMostActives;
        return stocksMostActives;
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}