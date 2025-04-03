using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CryptoTrade.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public DateTime Birthdate { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Required]
    public string? DNI { get; set; }

    [Required]
    public string? Nationality { get; set; }
    
    public double Cash { get; set; } = 0;

    public double Wallet { get; set; } = 0;

    public string Language { get; set; } = "ES";

    public string Currency { get; set; } = "USD";

    public string Theme { get; set; } = "light";

    public bool IsBanned { get; set; } = false;

    [JsonIgnore]
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

    [Required]
    public string Role { get; set; } = Roles.User;

    public User() {}

    public User(string name, DateTime birthdate, string email, string password, string phone, string dni, string nationality) 
    {
        Name = name;
        Birthdate = birthdate;
        Email = email;
        Password = password;
        Phone = phone;
        DNI = dni;
        Nationality = nationality;
    }
}
