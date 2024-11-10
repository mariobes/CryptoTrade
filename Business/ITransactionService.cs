using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface ITransactionService
{
    public IEnumerable<Transaction> GetAllTransactions(int userId);
    public void MakeDeposit(DepositDTO depositDTO);
    public void MakeWithdrawal(WithdrawalDTO withdrawalDTO);
    public void BuyCrypto(BuySellAssetDTO buySellAssetDTO);
    public void SellCrypto(BuySellAssetDTO buySellAssetDTO);
    public void BuyStock(BuySellAssetDTO buySellAssetDTO);
    public void SellStock(BuySellAssetDTO buySellAssetDTO);
    public void CryptoConverter(CryptoConverterDTO cryptoConverterDTO);
    public Dictionary<string, double> MyCryptos(int userId);
    public Dictionary<string, double> MyStocks(int userId);
}
