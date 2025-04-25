using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class StockTrending
{
    [Key]
    public string? Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Symbol { get; set; }

    public string? Image { get; set; }

    public double? Price { get; set; }

    public double? ChangePercentage { get; set; }

    [Required]
    public DateTime LastUpdated { get; set; }

    public StockTrending() {}

    public StockTrending (string id, string name, string symbol, string image, double price, double changePercentage, DateTime lastUpdated)
    {
        Id = id;
        Name = name;
        Symbol = symbol;
        Image = image;
        Price = price;
        ChangePercentage = changePercentage;
        LastUpdated = lastUpdated;
    }

}
