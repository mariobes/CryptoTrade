using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
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

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow.AddHours(2);

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

    public static class PasswordHasher
    {
        public static string Hash(string? password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool Verify(string inputPassword, string storedHashedPassword)
        {
            var hashedInput = Hash(inputPassword);
            return hashedInput == storedHashedPassword;
        }
    }

    public bool IsAdult()
    {
        var age = DateTime.UtcNow.Year - Birthdate.Year;
        if (DateTime.UtcNow.DayOfYear < Birthdate.DayOfYear)
        {
            age--;
        }
        return age >= 18;
    }
}
