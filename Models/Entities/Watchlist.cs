using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class Watchlist
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string AssetId { get; set; }

    [Required]
    public string TypeAsset { get; set; }


    public Watchlist() {}

    public Watchlist(int userId, string assetId, string typeAsset) 
    {
        UserId = userId;
        AssetId = assetId;
        TypeAsset = typeAsset;
    }
}