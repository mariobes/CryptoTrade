using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class UserUpdateDto
{
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string? Email { get; set; }

    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\""{}|<>])[A-Za-z\d!@#$%^&*(),.?\""{}|<>]{8,}$", 
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, 1 letra mayúscula, 1 número y 1 carácter especial")]
    public string? Password { get; set; }

    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    public string? Phone { get; set; }
}