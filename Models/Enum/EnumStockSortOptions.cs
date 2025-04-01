using System.ComponentModel.DataAnnotations;

public enum EnumStockSortOptions
{
    [Display(Name = "Market Cap")]
    marketCap = 0,

    [Display(Name = "Name")]
    name = 1,
}