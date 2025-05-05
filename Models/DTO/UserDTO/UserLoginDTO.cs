using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class UserLoginDto
{
    [Required(ErrorMessage = "Debes introducir un correo electrónico o un número de teléfono")]
    public string? EmailOrPhone { get; set; }
    
    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\""{}|<>])[A-Za-z\d!@#$%^&*(),.?\""{}|<>]{8,}$", 
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, 1 letra mayúscula, 1 número y 1 carácter especial")]
    public string? Password { get; set; }
}