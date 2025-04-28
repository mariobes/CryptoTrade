
namespace CryptoTrade.Models;

public class UserAssetsSummaryDto
{
    public string? AssetId { get; set; }
    public string? Name { get; set; }
    public double TotalInvestedAmount { get; set; }
    public double TotalAssetAmount { get; set; }
    public double AveragePurchasePrice { get; set; }
    public double Balance { get; set; }
    public double BalancePercentage { get; set; }
    public double Total { get; set; }
    public double WalletPercentage { get; set; }
}


