using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class CryptoIndexDto
{
    [Required]
    public string? Name { get; set; }

    public double? Value { get; set; }

    public double? ChangePercentage { get; set; }

    public string? Sentiment { get; set; }
}
