using System.ComponentModel.DataAnnotations;

public enum EnumSortOptions
{
    [Display(Name = "Market Cap")]
    marketCap = 0,

    [Display(Name = "Name")]
    name = 1,
}