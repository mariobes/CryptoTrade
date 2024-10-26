using System.ComponentModel.DataAnnotations;

namespace CryptoTrade.Models;

public class Stock
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.Now;

    [Required]
    public string? Description { get; set; }

    [Required]
    public int Ranking { get; set; }

    [Required]
    public string? Website { get; set; }

    [Required]
    public double CompanyValue { get; set; }

    [Required]
    public double EarningPerShare { get; set; }

    [Required]
    public string? Category { get; set; }

    [Required]
    public double DividendYield { get; set; }

    public Stock() {}

    public Stock(string name, double value, string description, int ranking, string website, double companyValue, double earningPerShare, double dividendYield) 
    {
        Name = name;
        Value = value;
        Description = description;
        Ranking = ranking;
        Website = website;
        CompanyValue = companyValue;
        EarningPerShare = earningPerShare;
        DividendYield = dividendYield;
    }
}
