using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class AssetMarketDto
{
    [Required]
    public string? Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Symbol { get; set; }

    public string? Image { get; set; }

    public double? Price { get; set; }

    public double? ChangePercentage { get; set; }
}