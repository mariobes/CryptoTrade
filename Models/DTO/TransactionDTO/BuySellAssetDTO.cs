using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class BuySellAssetDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Required]
    public string? AssetId { get; set; }

    public double? Amount { get; set; }
    
    public double? AssetAmount  { get; set; }
}