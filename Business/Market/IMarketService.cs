using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IMarketService
{
    public Task DeleteCryptoIndexDatabase();
    public Task UpdateCryptoIndexDatabase(CryptoIndexDto cryptoIndex);
    public IEnumerable<CryptoIndex> GetCryptoIndices();

    public Task UpdateCryptoTrendingDatabase(List<AssetMarketDto> cryptosTrending);
    public IEnumerable<CryptoTrending> GetCryptosTrending();

    public Task UpdateStockTrendingDatabase(List<AssetMarketDto> stocksTrending);
    public IEnumerable<StockTrending> GetStocksTrending();

    public Task UpdateStockGainerDatabase(List<AssetMarketDto> stocksGainer);
    public IEnumerable<StockGainer> GetStocksGainers();

    public Task UpdateStockLoserDatabase(List<AssetMarketDto> stocksLoser);
    public IEnumerable<StockLoser> GetStocksLosers();

    public Task UpdateStockMostActiveDatabase(List<AssetMarketDto> stocksMostActive);
    public IEnumerable<StockMostActive> GetStocksMostActives();
}
