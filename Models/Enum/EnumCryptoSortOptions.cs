using System.ComponentModel.DataAnnotations;

public enum EnumSortOptions
{
    [Display(Name = "Market Cap")]
    marketCap = 0,

    [Display(Name = "Market Cap Rank")]
    marketCapRank = 1,

    [Display(Name = "Name")]
    name = 2,

    [Display(Name = "Price")]
    price = 3,

    [Display(Name = "Price Change Percentage 1h")]
    priceChangePercentage1h = 4,

    [Display(Name = "Price Change Percentage 24h")]
    priceChangePercentage24h = 5,

    [Display(Name = "Price Change Percentage 7d")]
    priceChangePercentage7d = 6,

    [Display(Name = "Total Volume")]
    totalVolume = 7,

    [Display(Name = "Circulating Supply")]
    circulatingSupply = 8,
}