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
    public Dictionary<string, double> MyCryptos(int userId);
    public Dictionary<string, double> MyStocks(int userId);
}
