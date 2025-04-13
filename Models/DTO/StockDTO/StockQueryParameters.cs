using System.ComponentModel;

namespace CryptoTrade.Models;

public class StockQueryParameters
{
    [DefaultValue(EnumStockSortOptions.marketCap)]
    public EnumStockSortOptions SortBy { get; set; } = EnumStockSortOptions.marketCap;

    [DefaultValue(EnumOrderOptions.desc)]
    public EnumOrderOptions Order { get; set; } = EnumOrderOptions.desc;
}