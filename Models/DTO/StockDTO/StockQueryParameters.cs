using System.ComponentModel;

namespace CryptoTrade.Models;

public class StockQueryParameters
{

    [DefaultValue(EnumSortOptions.marketCap)]
    public EnumSortOptions SortBy { get; set; } = EnumSortOptions.marketCap;


    [DefaultValue(EnumOrderOptions.desc)]
    public EnumOrderOptions Order { get; set; } = EnumOrderOptions.desc;
}