using CryptoTrade.Models;
using System.Text.Json;

namespace CryptoTrade.Data;

public class TransactionJsonRepository : ITransactionRepository
{
    private Dictionary<string, Transaction> _transactions = new Dictionary<string, Transaction>();
    private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Data", "JsonData", "Transactions.json");
    private static int TransactionIdSeed { get; set; }

    public TransactionJsonRepository()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var transactions = JsonSerializer.Deserialize<IEnumerable<Transaction>>(jsonString);
                _transactions = transactions.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the transaction file", e);
            }
        }

        TransactionIdSeed = _transactions.Any() ? _transactions.Values.Max(u => u.Id) + 1 : 1;
    }

    public void AddTransaction(Transaction transaction)
    {
        transaction.Id = TransactionIdSeed++;
        _transactions[transaction.Id.ToString()] = transaction;
        SaveChanges();
    }

    public IEnumerable<Transaction> GetAllTransactions(int userId) 
    {
        return _transactions.Values.Where(t => t.UserId == userId);
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_transactions.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the transaction file", e);
        }
    }
}