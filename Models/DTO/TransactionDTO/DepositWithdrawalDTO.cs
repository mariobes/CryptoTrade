using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class DepositWithdrawalDTO
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario no es válido")]
    public int UserId { get; set; }

    [Required]
    [Range(10, double.MaxValue, ErrorMessage = "El saldo inicial debe ser mayor que 10")]
    public double Amount { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "El método de pago debe tener menos de 30 caracteres")]
    public string? PaymentMethod { get; set; }
}