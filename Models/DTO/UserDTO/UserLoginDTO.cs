using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class UserLoginDto
{
    [Required]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string? Email { get; set; }
    
    [Required]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 carácteres")]
    public string? Password { get; set; }
}