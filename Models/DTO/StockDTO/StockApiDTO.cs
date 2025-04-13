using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CryptoTrade.Models;

public class StockApiDto
{
    [Required]
    [JsonPropertyName("companyName")]
    public string? Name { get; set; }

    [Required]
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("price")]
    [Required]
    public double? Price { get; set; }

    [JsonPropertyName("marketCap")]
    [Required]
    public double? MarketCap { get; set; }

    [JsonPropertyName("sector")]
    public string? Sector { get; set; }

    [JsonPropertyName("industry")]
    public string? Industry { get; set; }

    [JsonPropertyName("lastAnnualDividend")]
    public double? LastAnnualDividend { get; set; }

    [JsonPropertyName("volume")]
    public double? Volume { get; set; }

    [JsonPropertyName("exchange")]
    public string? Exchange { get; set; }

    [JsonPropertyName("exchangeShortName")]
    public string? ExchangeShortName { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("isEtf")]
    public bool? IsEtf { get; set; }

    [JsonPropertyName("isFund")]
    public bool? IsFund { get; set; }

    [JsonPropertyName("isActivelyTrading")]
    public bool? IsActivelyTrading { get; set; }
}
