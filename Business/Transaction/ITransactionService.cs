using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface ITransactionService
{
    public IEnumerable<Transaction> GetAllTransactions(int userId);
    public void MakeDeposit(DepositDto dto);
    public void MakeWithdrawal(WithdrawalDto dto);
    public void BuyCrypto(BuySellAssetDto dto);
    public void SellCrypto(BuySellAssetDto dto);
    public void BuyStock(BuySellAssetDto dto);
    public void SellStock(BuySellAssetDto dto);
    public IEnumerable<UserAssetsSummaryDto> MyAssets(int userId, string? typeAsset = null, string? assetId = null);
    public void UpdateAllUsersBalances();
}
