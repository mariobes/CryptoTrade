using CryptoTrade.Models;

namespace CryptoTrade.Data;

public class WatchlistEFRepository : IWatchlistRepository
{
    private readonly CryptoTradeContext _context;

    public WatchlistEFRepository(CryptoTradeContext context)
    {
        _context = context;
    }

    public void AddWatchlist(Watchlist watchlist)
    {
        _context.Watchlists.Add(watchlist);
        SaveChanges();
    }

    public void DeleteWatchlist(Watchlist watchlist) 
    {
        var watchlistToDelete = GetWatchlist(watchlist.UserId, watchlist.AssetId, watchlist.TypeAsset);
        _context.Watchlists.Remove(watchlistToDelete);
        SaveChanges();
    }

    public IEnumerable<Watchlist> GetAllWatchlists(int userId, string typeAsset) 
    {
        var watchlists = _context.Watchlists.Where(watchlist => watchlist.UserId == userId &&
                                                                watchlist.TypeAsset == typeAsset);
        return watchlists;
    }

    public Watchlist GetWatchlist(int userId, string assetId, string typeAsset) 
    {
        var watchlist = _context.Watchlists.FirstOrDefault(watchlist => watchlist.UserId == userId &&
                                                                        watchlist.AssetId == assetId &&
                                                                        watchlist.TypeAsset == typeAsset);
        return watchlist;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}