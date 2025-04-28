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
    public IEnumerable<UserAssetsSummaryDto> MyCryptos(int userId, string? cryptoId = null);
    public IEnumerable<UserAssetsSummaryDto> MyStocks(int userId, string? stockId = null);
}
