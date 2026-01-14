using CryptoTrade.Models;
using System.Text.Json;

namespace CryptoTrade.Data;

public class WatchlistJsonRepository : IWatchlistRepository
{
    private Dictionary<string, Watchlist> _watchlists = new Dictionary<string, Watchlist>();
    private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Data", "JsonData", "Watchlists.json");
    private static int WatchlistIdSeed { get; set; }

    public WatchlistJsonRepository()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var watchlists = JsonSerializer.Deserialize<IEnumerable<Watchlist>>(jsonString);
                _watchlists = watchlists.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the watchlist file", e);
            }
        }

        WatchlistIdSeed = _watchlists.Any() ? _watchlists.Values.Max(u => u.Id) + 1 : 1;
    }

    public void AddWatchlist(Watchlist watchlist)
    {
        watchlist.Id = WatchlistIdSeed++;
        _watchlists[watchlist.Id.ToString()] = watchlist;
        SaveChanges();
    }

    public void DeleteWatchlist(Watchlist watchlist) 
    {
        var watchlistToDelete = GetWatchlist(watchlist.UserId, watchlist.AssetId, watchlist.TypeAsset);
        _watchlists.Remove(watchlistToDelete.Id.ToString());
        SaveChanges();
    }

    public IEnumerable<Watchlist> GetAllWatchlists(int userId, string typeAsset) 
    {
        var watchlists = _watchlists.Values.Where(watchlist => watchlist.UserId == userId &&
                                                                watchlist.TypeAsset == typeAsset);
        return watchlists;
    }

    public Watchlist GetWatchlist(int userId, string assetId, string typeAsset) 
    {
        var watchlist = _watchlists.Values.FirstOrDefault(watchlist => watchlist.UserId == userId &&
                                                                        watchlist.AssetId == assetId &&
                                                                        watchlist.TypeAsset == typeAsset);
        return watchlist;
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_watchlists.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the watchlist file", e);
        }
    }
}