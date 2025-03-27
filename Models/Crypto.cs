using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Crypto
{
    [Key]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [Required]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [Required]
    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [Required]
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("current_price")]
    [Required]
    public double? Price { get; set; }

    [JsonPropertyName("market_cap")]
    [Required]
    public double? MarketCap { get; set; }

    [JsonPropertyName("fully_diluted_valuation")]
    public double? FullyDilutedValuation { get; set; }

    [JsonPropertyName("total_volume")]
    public double? TotalVolume { get; set; }

    [JsonPropertyName("high_24h")]
    public double? High24h { get; set; }

    [JsonPropertyName("low_24h")]
    public double? Low24h { get; set; }

    [JsonPropertyName("price_change_24h")]
    public double? PriceChange24h { get; set; }

    [JsonPropertyName("price_change_percentage_24h")]
    public double? PriceChangePercentage24h { get; set; }

    [JsonPropertyName("market_cap_change_24h")]
    public double? MarketCapChange24h { get; set; }

    [JsonPropertyName("market_cap_change_percentage_24h")]
    public double? MarketCapChangePercentage24h { get; set; }

    [JsonPropertyName("circulating_supply")]
    public double? CirculatingSupply { get; set; }

    [JsonPropertyName("total_supply")]
    public double? TotalSupply { get; set; }

    [JsonPropertyName("max_supply")]
    public double? MaxSupply { get; set; }

    [JsonPropertyName("ath")]
    public double? AllTimeHigh { get; set; }

    [JsonPropertyName("ath_change_percentage")]
    public double? AllTimeHighChangePercentage { get; set; }

    [JsonPropertyName("ath_date")]
    public DateTime? AllTimeHighDate { get; set; }

    [JsonPropertyName("atl")]
    public double? AllTimeLow { get; set; }

    [JsonPropertyName("atl_change_percentage")]
    public double? AllTimeLowChangePercentage { get; set; }

    [JsonPropertyName("atl_date")]
    public DateTime? AllTimeLowDate { get; set; }

    [Required]
    public DateTime LastUpdated { get; set; }


    public Crypto() {}

    public Crypto(string id, string name, string symbol, string image, double price, double marketCap, double fullyDilutedValuation, double totalVolume, double high24h, double low24h, double priceChange24h, double priceChangePercentage24h, double marketCapChange24h, double marketCapChangePercentage24h, double circulatingSupply, double totalSupply, double maxSupply, double allTimeHigh, double allTimeHighChangePercentage, DateTime allTimeHighDate, double allTimeLow, double allTimeLowChangePercentage, DateTime allTimeLowDate, DateTime lastUpdated)
    {
        Id = id;
        Name = name;
        Symbol = symbol;
        Image = image;
        Price = price;
        MarketCap = marketCap;
        FullyDilutedValuation = fullyDilutedValuation;
        TotalVolume = totalVolume;
        High24h = high24h;
        Low24h = low24h;
        PriceChange24h = priceChange24h;
        PriceChangePercentage24h = priceChangePercentage24h;
        MarketCapChange24h = marketCapChange24h;
        MarketCapChangePercentage24h = marketCapChangePercentage24h;
        CirculatingSupply = circulatingSupply;
        TotalSupply = totalSupply;
        MaxSupply = maxSupply;
        AllTimeHigh = allTimeHigh;
        AllTimeHighChangePercentage = allTimeHighChangePercentage;
        AllTimeHighDate = allTimeHighDate;
        AllTimeLow = allTimeLow;
        AllTimeLowChangePercentage = allTimeLowChangePercentage;
        AllTimeLowDate = allTimeLowDate;
        LastUpdated = lastUpdated;
    }
}






// using System.ComponentModel.DataAnnotations;

// namespace CryptoTrade.Models;

// public class Crypto
// {
//     [Key]
//     public int Id { get; set; }

//     [Required]
//     public string? Name { get; set; }

//     [Required]
//     public string? Symbol { get; set; }

//     [Required]
//     public double MarketCap { get; set; }

//     [Required]
//     public DateTime CreationDate { get; set; } = DateTime.Now;

//     [Required]
//     public string? Description { get; set; }

//     [Required]
//     public double Value { get; set; }

//     [Required]
//     public string? Website { get; set; }

//     [Required]
//     public double TotalSupply { get; set; }

//     [Required]
//     public double CirculatingSupply { get; set; }
    
//     [Required]
//     public string? Contract { get; set; }

//     [Required]
//     public double AllTimeHigh { get; set; }

//     [Required]
//     public double AllTimeLow { get; set; }

//     public Crypto() {}

//     public Crypto(string name, string symbol, double marketCap, string description, double value, string website, double totalSupply, double circulatingSupply, string contract, double allTimeHigh, double allTimeLow) 
//     {
//         Name = name;
//         Symbol = symbol;
//         MarketCap = marketCap;
//         Description = description;
//         Value = value;
//         Website = website;
//         TotalSupply = totalSupply;
//         CirculatingSupply = circulatingSupply;
//         Contract = contract;
//         AllTimeHigh = allTimeHigh;
//         AllTimeLow = allTimeLow;
//     }
// }
