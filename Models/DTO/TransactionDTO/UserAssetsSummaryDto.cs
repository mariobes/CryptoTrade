
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
    public string? TypeOfAsset { get; set; }
    public string? Symbol { get; set; }
    public string? Image { get; set; }
    public double Price { get; set; }
    public double? ChangesPercentage24h { get; set; }
}


