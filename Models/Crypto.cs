using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class Crypto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Symbol { get; set; }

    [Required]
    public string? MarketCap { get; set; }

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    [Required]
    public string? Description { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public int Ranking { get; set; }

    [Required]
    public string? Website { get; set; }

    [Required]
    public double TotalSupply { get; set; }

    [Required]
    public double CirculatingSupply { get; set; }
    
    [Required]
    public string? Contract { get; set; }

    [Required]
    public double AllTimeHigh { get; set; }

    [Required]
    public double AllTimeLow { get; set; }

    public Crypto() {}

    public Crypto(string name, string symbol, string marketCap, string description, double value, int ranking, string website, double totalSupply, double circulatingSupply, string contract, double allTimeHigh, double allTimeLow) 
    {
        Name = name;
        Symbol = symbol;
        MarketCap = marketCap;
        Description = description;
        Value = value;
        Ranking = ranking;
        Website = website;
        TotalSupply = totalSupply;
        CirculatingSupply = circulatingSupply;
        Contract = contract;
        AllTimeHigh = allTimeHigh;
        AllTimeLow = allTimeLow;
    }
}
