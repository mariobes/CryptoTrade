using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class UserCreateDTO
{
    [Required]
    [StringLength(50, ErrorMessage = "El nombre debe tener menos de 50 caracteres")]
    public string? Name { get; set; }

    [Required]
    public DateTime Birthdate { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string? Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    public string? Password { get; set; }

    [Required]
    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    public string? Phone { get; set; }

    [Required]
    public string? DNI { get; set; }

    [Required]
    public string? Nationality { get; set; }
}