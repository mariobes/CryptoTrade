using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class CryptoIndex
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public double? Value { get; set; }

    public double? ChangePercentage { get; set; }

    public string? Sentiment { get; set; }

    [Required]
    public DateTime LastUpdated { get; set; }

    public CryptoIndex() {}

    public CryptoIndex (string name, double value, double changePercentage, DateTime lastUpdated)
    {
        Name = name;
        Value = value;
        ChangePercentage = changePercentage;
        LastUpdated = lastUpdated;
    }

    public CryptoIndex (string name, double value, string sentiment, DateTime lastUpdated)
    {
        Name = name;
        Value = value;
        Sentiment = sentiment;
        LastUpdated = lastUpdated;
    }

}
