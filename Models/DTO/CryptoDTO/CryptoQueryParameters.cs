using System.ComponentModel;

namespace CryptoTrade.Models;

public class CryptoQueryParameters
{

    [DefaultValue(EnumSortOptions.value)]
    public EnumSortOptions SortBy { get; set; } = EnumSortOptions.value;


    [DefaultValue(EnumOrderOptions.desc)]
    public EnumOrderOptions Order { get; set; } = EnumOrderOptions.desc;
}