using System.ComponentModel.DataAnnotations;

public enum EnumStockSortOptions
{
    [Display(Name = "Market Cap")]
    marketCap = 0,

    [Display(Name = "Market Cap Rank")]
    marketCapRank = 1,

    [Display(Name = "Name")]
    name = 2,

    [Display(Name = "Price")]
    price = 3,

    [Display(Name = "Price Change 24h")]
    priceChange24h = 4,

    [Display(Name = "Price Change Percentage 24h")]
    priceChangePercentage24h = 5,

    [Display(Name = "Currency")]
    currency = 6,

    [Display(Name = "Last Dividend")]
    lastDividend = 7,

    [Display(Name = "Average Volume")]
    averageVolume = 8


}