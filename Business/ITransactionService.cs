using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface ITransactionService
{
    public IEnumerable<Transaction> GetAllTransactions(int userId);
    public void MakeDeposit(DepositDTO dto);
    public void MakeWithdrawal(WithdrawalDTO dto);
    public void BuyCrypto(BuySellAssetDTO dto);
    public void SellCrypto(BuySellAssetDTO dto);
    public void BuyStock(BuySellAssetDTO dto);
    public void SellStock(BuySellAssetDTO dto);
    public void CryptoConverter(CryptoConverterDTO dto);
    public Dictionary<string, double> MyCryptos(int userId);
    public Dictionary<string, double> MyStocks(int userId);
}
