using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class UserCreateDto
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
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\""{}|<>])[A-Za-z\d!@#$%^&*(),.?\""{}|<>]{8,}$", 
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, 1 letra mayúscula, 1 número y 1 carácter especial.")]
    public string? Password { get; set; }

    [Required]
    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    public string? Phone { get; set; }

    [Required]
    [RegularExpression(@"^\d{8}[A-Za-z]$", ErrorMessage = "El DNI no es válido.")]
    public string? DNI { get; set; }

    [Required]
    public string? Nationality { get; set; }
}