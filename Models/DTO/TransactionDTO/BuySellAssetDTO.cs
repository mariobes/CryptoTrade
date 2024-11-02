using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class BuySellAsset
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del activo no es válido")]
    public int AssetId { get; set; }

    [Required]
    [Range(10, double.MaxValue, ErrorMessage = "La cantidad mínima es 10")]
    public double Amount { get; set; }
}