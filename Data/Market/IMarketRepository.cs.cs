using CryptoTrade.Models;

namespace CryptoTrade.Data;

public interface IMarketRepository
{
    public void AddCryptoIndex(CryptoIndex cryptoIndex);
    public void DeleteCryptoIndex();
    IEnumerable<CryptoIndex> GetCryptoIndices();

    public void AddCryptoTrending(CryptoTrending cryptoTrending);
    public void DeleteCryptoTrending();
    IEnumerable<CryptoTrending> GetCryptosTrending();

    public void AddStockTrending(StockTrending stockTrending);
    public void DeleteStockTrending();
    IEnumerable<StockTrending> GetStocksTrending();

    public void AddStockGainer(StockGainer stockGainer);
    public void DeleteStockGainer();
    IEnumerable<StockGainer> GetStocksGainers();

    public void AddStockLoser(StockLoser stockLoser);
    public void DeleteStockLoser();
    IEnumerable<StockLoser> GetStocksLosers();

    public void AddStockMostActive(StockMostActive stockMostActive);
    public void DeleteStockMostActive();
    IEnumerable<StockMostActive> GetStocksMostActives();

    void SaveChanges();
}
