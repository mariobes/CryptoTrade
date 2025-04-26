using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IWatchlistService
{
    public Watchlist RegisterWatchlist(WatchlistCreateDto dto);
    public void DeleteWatchlist(WatchlistCreateDto dto);
    public IEnumerable<Watchlist> GetAllWatchlists(int userId, string typeAsset);
}

