using System.ComponentModel.DataAnnotations;

public enum EnumStockSortOptions
{
    [Display(Name = "Price")]
    value = 0,

    [Display(Name = "Name")]
    name = 1,
    
    [Display(Name = "Company Value")]
    companyValue = 2
}