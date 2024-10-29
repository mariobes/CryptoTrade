using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class CryptoCreateUpdateDTO
{
    [Required]
    [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 caracteres")]
    public string? Name { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "La abreviatura debe tener menos de 10 caracteres")]
    public string? Symbol { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "La capitalización de mercado debe tener menos de 20 caracteres")]
    public string? MarketCap { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "La descripción debe tener menos de 500 caracteres")]
    public string? Description { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El valor no puede ser negativo")]
    public double Value { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "La posición no puede ser negativa")]
    public int Ranking { get; set; }

    [Required]
    [Url(ErrorMessage = "Debe ser una URL válida")]
    public string? Website { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El suministro total no puede ser negativo")]
    public double TotalSupply { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El suministro circulante no puede ser negativo")]
    public double CirculatingSupply { get; set; }
    
    [Required]
    [RegularExpression(@"^0x[a-fA-F0-9]{40}$", ErrorMessage = "El contrato debe ser una dirección hexadecimal válida")]
    public string? Contract { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El máximo histórico no puede ser negativo")]
    public double AllTimeHigh { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El mínimo histórico no puede ser negativo")]
    public double AllTimeLow { get; set; }
}