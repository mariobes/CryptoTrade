using System.ComponentModel.DataAnnotations;

public enum EnumOrderOptions
{
    [Display(Name = "Descending")]
    desc = 0,

    [Display(Name = "Ascending")]
    asc = 1
}