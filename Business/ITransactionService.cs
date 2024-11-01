using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface ITransactionService
{
    public IEnumerable<Transaction> GetAllTransactions(int userId);
    public void MakeDeposit(DepositWithdrawalDTO depositWithdrawalDTO);
    public void MakeWithdrawal(DepositWithdrawalDTO depositWithdrawalDTO);
    public void BuyCrypto(BuySellCrypto buySellCrypto);
    public void SellCrypto(BuySellCrypto buySellCrypto);
}
