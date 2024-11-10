using System.ComponentModel.DataAnnotations;

public enum EnumSortOptions
{
    [Display(Name = "Value")]
    value = 0,

    [Display(Name = "Name")]
    name = 1,
    
    [Display(Name = "Market Cap")]
    marketCap = 2
}