using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CryptoTrade.Models;

public class StockCreateUpdateDto
{
    [JsonIgnore]
    public string? Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "El nombre debe tener menos de 100 caracteres")]
    public string? Name { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "El símbolo debe tener menos de 10 caracteres")]
    public string? Symbol { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
    public double? Price { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "La capitalización de mercado no puede ser negativa")]
    public double? MarketCap { get; set; }

    public string? Sector { get; set; }

    [Required]
    public string? Industry { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El dividendo anual no puede ser negativo")]
    public double? LastAnnualDividend { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El volumen no puede ser negativo")]
    public double? Volume { get; set; }

    public string? Exchange { get; set; }

    public string? ExchangeShortName { get; set; }

    public string? Country { get; set; }

    public double? Changes { get; set; }

    public string? Currency { get; set; }

    public string? Isin { get; set; }

    [Url(ErrorMessage = "Debe ser una URL válida")]
    public string? Website { get; set; }

    public string? Description { get; set; }

    public string? Ceo { get; set; }

    [Url(ErrorMessage = "Debe ser una URL válida")]
    public string? Image { get; set; }

    [Required]
    public DateTime LastUpdated { get; set; }
}
