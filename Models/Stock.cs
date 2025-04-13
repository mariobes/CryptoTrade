using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Stock
{
    [Key]
    public string? Id { get; set; }

    [Required]
    [JsonPropertyName("companyName")]
    public string? Name { get; set; }

    [Required]
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [Required]
    [JsonPropertyName("price")]
    public double? Price { get; set; }

    [Required]
    [JsonPropertyName("mktCap")]
    public double? MarketCap { get; set; }

    public double? MarketCapRank { get; set; }

    [JsonPropertyName("sector")]
    public string? Sector { get; set; }

    [JsonPropertyName("industry")]
    public string? Industry { get; set; }

    [JsonPropertyName("lastDiv")]
    public double? LastAnnualDividend { get; set; }

    [JsonPropertyName("volAvg")]
    public double? Volume { get; set; }

    [JsonPropertyName("exchange")]
    public string? Exchange { get; set; }

    [JsonPropertyName("exchangeShortName")]
    public string? ExchangeShortName { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("changes")]
    public double? Changes { get; set; }

    public double? ChangesPercentage { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("isin")]
    public string? Isin { get; set; }

    [JsonPropertyName("website")]
    public string? Website { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("ceo")]
    public string? Ceo { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [Required]
    public DateTime LastUpdated { get; set; }


    public Stock() {}

    public Stock(string id, string name, string symbol, double price, double marketCap, string sector, string industry,
                 double lastAnnualDividend, double volume, string exchange, string exchangeShortName, string country, double changes,
                 double changesPercentage, string currency, string isin, string website, string description, string ceo, string image, DateTime lastUpdated)
    {
        Id = id;
        Name = name;
        Symbol = symbol;
        Price = price;
        MarketCap = marketCap;
        Sector = sector;
        Industry = industry;
        LastAnnualDividend = lastAnnualDividend;
        Volume = volume;
        Exchange = exchange;
        ExchangeShortName = exchangeShortName;
        Country = country;
        Changes = changes;
        ChangesPercentage = changesPercentage;
        Currency = currency;
        Isin = isin;
        Website = website;
        Description = description;
        Ceo = ceo;
        Image = image;
        LastUpdated = lastUpdated;
    }
}
