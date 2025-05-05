using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class UserUpdateDto
{
    [Required]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\""{}|<>])[A-Za-z\d!@#$%^&*(),.?\""{}|<>]{8,}$", 
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, 1 letra mayúscula, 1 número y 1 carácter especial")]
    public string? Password { get; set; }

    [Required]
    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    public string? Phone { get; set; }
}