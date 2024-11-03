using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class CryptoConverterDTO
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la criptomoneda no es válido")]
    public int CryptoId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la criptomoneda no es válido")]
    public int NewCryptoId { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El valor no puede ser negativo")]
    public double Amount { get; set; }
}


