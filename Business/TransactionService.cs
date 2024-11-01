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

        Transaction transaction = new(buySellCrypto.UserId, buySellCrypto.CryptoId, $"Comprar {crypto.Name}", buySellCrypto.Amount);
        user.Cash -= transaction.Amount;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }
    
}