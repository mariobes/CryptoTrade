using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CryptoTrade.Models;

public class StockChartDto
{
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("close")]
    public double? Price { get; set; }

    [JsonPropertyName("volume")]
    public double? Volume { get; set; }
}
