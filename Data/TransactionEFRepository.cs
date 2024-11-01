using CryptoTrade.Models;

namespace CryptoTrade.Data
{
    public class TransactionEFRepository : ITransactionRepository
    {
        private readonly CryptoTradeContext _context;

        public TransactionEFRepository(CryptoTradeContext context)
        {
            _context = context;
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            SaveChanges();
        }

        public IEnumerable<Transaction> GetAllTransactions(int userId) 
        {
            var transaction = _context.Transactions.Where(t => t.UserId == userId);
            return transaction;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }   
}