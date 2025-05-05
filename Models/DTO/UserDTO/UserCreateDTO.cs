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
        ErrorMessage = "La contraseña debe tener al menos 8 caracteres, 1 letra mayúscula, 1 número y 1 carácter especial")]
    public string? Password { get; set; }

    [Required]
    [RegularExpression(@"^\+?[0-9\s\-().]{7,20}$", ErrorMessage = "El número de teléfono no es válido")]
    public string? Phone { get; set; }

    [Required]
    [RegularExpression(@"^[\w\s\-\/\.]{4,30}$", ErrorMessage = "El documento de identidad no es válido")]
    public string? DNI { get; set; }

    [Required]
    public string? Nationality { get; set; }
}