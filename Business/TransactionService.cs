using CryptoTrade.Data;
using CryptoTrade.Models;

namespace CryptoTrade.Business;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICryptoRepository _cryptoRepository;

    public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ICryptoRepository cryptoRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _cryptoRepository = cryptoRepository;
    }

    public IEnumerable<Transaction> GetAllTransactions(int userId)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        return _transactionRepository.GetAllTransactions(userId);
    }

    public void MakeDeposit(DepositWithdrawalDTO depositWithdrawalDTO)
    {
        var user = _userRepository.GetUser(depositWithdrawalDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {depositWithdrawalDTO.UserId} no encontrado");
        }

        Transaction transaction = new(depositWithdrawalDTO.UserId, "Ingresar dinero", depositWithdrawalDTO.Amount, depositWithdrawalDTO.PaymentMethod);
        user.Cash += depositWithdrawalDTO.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void MakeWithdrawal(DepositWithdrawalDTO depositWithdrawalDTO)
    {
        var user = _userRepository.GetUser(depositWithdrawalDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {depositWithdrawalDTO.UserId} no encontrado");
        }

        if (user.Cash < depositWithdrawalDTO.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar el retiro");
        }

        Transaction transaction = new(depositWithdrawalDTO.UserId, "Retirar dinero", depositWithdrawalDTO.Amount, depositWithdrawalDTO.PaymentMethod);
        user.Cash -= depositWithdrawalDTO.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void BuyCrypto(BuySellCrypto buySellCrypto)
    {
        var user = _userRepository.GetUser(buySellCrypto.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellCrypto.UserId} no encontrado");
        }

        var crypto = _cryptoRepository.GetCrypto(buySellCrypto.CryptoId);
        if (crypto == null) 
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {buySellCrypto.CryptoId} no encontrada");
        }

        if (user.Cash < buySellCrypto.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new(buySellCrypto.UserId, buySellCrypto.CryptoId, $"Comprar {crypto.Name}", buySellCrypto.Amount, "Crypto");
        user.Cash -= transaction.Amount;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellCrypto(BuySellCrypto buySellCrypto)
    {
        var user = _userRepository.GetUser(buySellCrypto.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellCrypto.UserId} no encontrado");
        }

        var crypto = _cryptoRepository.GetCrypto(buySellCrypto.CryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {buySellCrypto.CryptoId} no encontrada");
        } 

        var userHasCrypto = HasCrypto(buySellCrypto.UserId, buySellCrypto.CryptoId);
        if (!userHasCrypto)
        {
            throw new Exception($"El usuario {buySellCrypto.UserId} no tiene la criptomoneda {buySellCrypto.CryptoId}");
        }

        var userCryptoBalance = GetCryptoBalance(buySellCrypto.UserId, buySellCrypto.CryptoId);
        if (buySellCrypto.Amount > userCryptoBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new(buySellCrypto.UserId, buySellCrypto.CryptoId, $"Vender {crypto.Name}", buySellCrypto.Amount, "Crypto");
        user.Cash += transaction.Amount;
        user.Wallet -= transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public bool HasCrypto(int userId, int cryptoId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId);

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId == cryptoId && transaction.TypeOfAsset.Equals("Crypto"))
            {
                return true;
            }
        }
        return false;
    }

    public double GetCryptoBalance(int userId, int cryptoId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId);

        double balance = 0;

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId == cryptoId && transaction.TypeOfAsset.Equals("Crypto"))
            {
                if (transaction.Concept.StartsWith("Comprar"))
                {
                    balance += transaction.Amount;
                }
                else if (transaction.Concept.StartsWith("Vender"))
                {
                    balance -= transaction.Amount;
                }
            }
        }
        return balance;
    }
    
}