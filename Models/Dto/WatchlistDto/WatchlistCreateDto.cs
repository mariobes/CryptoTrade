using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CryptoTrade.Models;

public class WatchlistCreateDto
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string AssetId { get; set; }

    [Required]
    public string TypeAsset { get; set; }
}
