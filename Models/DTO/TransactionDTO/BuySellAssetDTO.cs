using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class BuySellAssetDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Required]
    public string? AssetId { get; set; }

    [Required]
    [Range(10, double.MaxValue, ErrorMessage = "La cantidad mínima es 1")]
    public double Amount { get; set; }
}