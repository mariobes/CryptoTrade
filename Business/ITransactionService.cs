using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface ITransactionService
{
    public IEnumerable<Transaction> GetAllTransactions(int userId);
    public void MakeDeposit(DepositWithdrawalDTO depositWithdrawalDTO);
    public void MakeWithdrawal(DepositWithdrawalDTO depositWithdrawalDTO);
    public void BuyCrypto(BuySellAsset buySellAsset);
    public void SellCrypto(BuySellAsset buySellAsset);
    public void BuyStock(BuySellAsset buySellAsset);
    public void SellStock(BuySellAsset buySellAsset);
    public void CryptoConverter(CryptoConverterDTO cryptoConverterDTO);
    public Dictionary<string, double> MyCryptos(int userId);
}
