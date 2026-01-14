using System.ComponentModel;

namespace CryptoTrade.Models;

public class CryptoQueryParameters
{
    [DefaultValue(EnumCryptoSortOptions.marketCap)]
    public EnumCryptoSortOptions SortBy { get; set; } = EnumCryptoSortOptions.marketCap;

    [DefaultValue(EnumOrderOptions.desc)]
    public EnumOrderOptions Order { get; set; } = EnumOrderOptions.desc;
}