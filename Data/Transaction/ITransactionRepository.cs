using CryptoTrade.Models;

namespace CryptoTrade.Data;

public interface ITransactionRepository
{
    public void AddTransaction(Transaction transaction);
    public IEnumerable<Transaction> GetAllTransactions(int userId);
    void SaveChanges();
}
