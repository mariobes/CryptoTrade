using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class BuySellAssetDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Required]
    public string? AssetId { get; set; }


    [Range(1, double.MaxValue, ErrorMessage = "La cantidad mínima es 1")]
    public double? Amount { get; set; }
    

    [Range(0.000000000001, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
    public double? AssetAmount  { get; set; }
}