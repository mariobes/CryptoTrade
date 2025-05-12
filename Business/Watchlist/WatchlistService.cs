using CryptoTrade.Data;
using CryptoTrade.Models;

namespace CryptoTrade.Business;

public class WatchlistService : IWatchlistService
{
    private readonly IWatchlistRepository _repository;

    public WatchlistService(IWatchlistRepository repository)
    {
        _repository = repository;
    }

    public Watchlist RegisterWatchlist(WatchlistCreateDto dto)
    {
        Watchlist watchlist = new Watchlist
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            TypeAsset = dto.TypeAsset
        };
        _repository.AddWatchlist(watchlist);
        return watchlist;
    }

    public void DeleteWatchlist(WatchlistCreateDto dto)
    {
        Watchlist watchlist = new Watchlist
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            TypeAsset = dto.TypeAsset
        };
        _repository.DeleteWatchlist(watchlist);
    }

    public IEnumerable<Watchlist> GetAllWatchlists(int userId, string typeAsset)
    {
        return _repository.GetAllWatchlists(userId, typeAsset);
    }
}
