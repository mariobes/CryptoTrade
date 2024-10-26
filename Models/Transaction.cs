using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CryptoTrade.Models;

public class Transaction
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public int? AssetId { get; set; }

    [Required]
    public string? Concept { get; set; }

    [Required]
    public double Amount { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;

    //public double Charge { get; set; } = 1.0;

    public string? PaymentMethod { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

    [JsonIgnore]
    public Crypto? Crypto { get; set; }

    [JsonIgnore]
    public Stock? Stock { get; set; }

    public Transaction() {}

    public Transaction (int userId, string concept, double amount, string paymentMethod)
    {
        UserId = userId;
        Concept = concept;
        Amount = amount;
        PaymentMethod = paymentMethod;
    }

    public Transaction (int userId, int assetId, string concept, double amount)
    {
        UserId = userId;
        AssetId = assetId;
        Concept = concept;
        Amount = amount;
    }

}
