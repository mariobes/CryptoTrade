using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class StockCreateUpdateDTO
{
    [Required]
    [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 caracteres")]
    public string? Name { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El valor no puede ser negativo")]
    public double Value { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "La descripción debe tener menos de 500 caracteres")]
    public string? Description { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "La posición no puede ser negativa")]
    public int Ranking { get; set; }

    [Required]
    [Url(ErrorMessage = "Debe ser una URL válida")]
    public string? Website { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El valor de la compañía no puede ser negativo")]
    public double CompanyValue { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "La ganancia por acción no puede ser negativa")]
    public double EarningPerShare { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "La categoría debe tener menos de 50 caracteres")]
    public string? Category { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El rendimiento por acción no puede ser negativo")]
    public double DividendYield { get; set; }
}