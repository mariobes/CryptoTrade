using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class CryptoCreateUpdateDto
{
    [Required]
    public string? Id { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 caracteres")]
    [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios")]
    public string? Name { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "El símbolo debe tener menos de 10 caracteres")]
    public string? Symbol { get; set; }

    [Required]
    [Url(ErrorMessage = "Debe ser una URL válida")]
    public string? Image { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
    public double? Price { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "La capitalización de mercado no puede ser negativa")]
    public double? MarketCap { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "La valoración totalmente diluida no puede ser negativo")]
    public double? FullyDilutedValuation { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El volumen total de operaciones no puede ser negativo")]
    public double? TotalVolume { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio más alto en 24h no puede ser negativo")]
    public double? High24h { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio más bajo en 24h no puede ser negativo")]
    public double? Low24h { get; set; }

    [Range(double.MinValue, double.MaxValue, ErrorMessage = "El cambio de precio en 24h no puede ser nulo")]
    public double? PriceChange24h { get; set; }

    [Range(double.MinValue, double.MaxValue, ErrorMessage = "El cambio porcentual en 24h no puede ser nulo")]
    public double? PriceChangePercentage24h { get; set; }

    [Range(double.MinValue, double.MaxValue, ErrorMessage = "El cambio porcentual en 1h no puede ser nulo")]
    public double? PriceChangePercentage1h { get; set; }

    [Range(double.MinValue, double.MaxValue, ErrorMessage = "El cambio porcentual en 7d no puede ser nulo")]
    public double? PriceChangePercentage7d { get; set; }

    [Range(double.MinValue, double.MaxValue, ErrorMessage = "El cambio en capitalización de mercado en 24h no puede ser nulo")]
    public double? MarketCapChange24h { get; set; }

    [Range(double.MinValue, double.MaxValue, ErrorMessage = "El cambio porcentual en capitalización de mercado en 24h no puede ser nulo")]
    public double? MarketCapChangePercentage24h { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El suministro circulante no puede ser negativo")]
    public double? CirculatingSupply { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El suministro total no puede ser negativo")]
    public double? TotalSupply { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El suministro máximo no puede ser negativo")]
    public double? MaxSupply { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El máximo histórico no puede ser negativo")]
    public double? AllTimeHigh { get; set; }

    [Range(double.MinValue, double.MaxValue, ErrorMessage = "El cambio porcentual desde el máximo histórico no puede ser nulo")]
    public double? AllTimeHighChangePercentage { get; set; }

    public DateTime? AllTimeHighDate { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El mínimo histórico no puede ser negativo")]
    public double? AllTimeLow { get; set; }

    [Range(double.MinValue, double.MaxValue, ErrorMessage = "El cambio porcentual desde el mínimo histórico no puede ser nulo")]
    public double? AllTimeLowChangePercentage { get; set; }

    public DateTime? AllTimeLowDate { get; set; }

    public SparklineIn7d? SparklineIn7d { get; set; }
}