using CryptoTrade.Models;

namespace CryptoTrade.Data;

public interface IWatchlistRepository
{
    public void AddWatchlist(Watchlist watchlist);
    public void DeleteWatchlist(Watchlist watchlist);
    public IEnumerable<Watchlist> GetAllWatchlists(int userId, string typeAsset);
    public Watchlist GetWatchlist(int userId, string assetId, string typeAsset);
    void SaveChanges();
}
