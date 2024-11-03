using CryptoTrade.Data;
using CryptoTrade.Models;

namespace CryptoTrade.Business;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICryptoRepository _cryptoRepository;
    private readonly IStockRepository _stockRepository;

    public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, ICryptoRepository cryptoRepository, IStockRepository stockRepository)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _cryptoRepository = cryptoRepository;
        _stockRepository = stockRepository;
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

    public void BuyCrypto(BuySellAsset buySellAsset)
    {
        var user = _userRepository.GetUser(buySellAsset.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellAsset.UserId} no encontrado");
        }

        var crypto = _cryptoRepository.GetCrypto(buySellAsset.AssetId);
        if (crypto == null) 
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {buySellAsset.AssetId} no encontrada");
        }

        if (user.Cash < buySellAsset.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new(buySellAsset.UserId, buySellAsset.AssetId, $"Comprar {crypto.Name}", buySellAsset.Amount, "Crypto");
        user.Cash -= transaction.Amount;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellCrypto(BuySellAsset buySellAsset)
    {
        var user = _userRepository.GetUser(buySellAsset.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellAsset.UserId} no encontrado");
        }

        var crypto = _cryptoRepository.GetCrypto(buySellAsset.AssetId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {buySellAsset.AssetId} no encontrada");
        } 

        var userHasCrypto = HasCrypto(buySellAsset.UserId, buySellAsset.AssetId);
        if (!userHasCrypto)
        {
            throw new Exception($"El usuario {buySellAsset.UserId} no tiene la criptomoneda {buySellAsset.AssetId}");
        }

        var userCryptoBalance = GetCryptoBalance(buySellAsset.UserId, buySellAsset.AssetId);
        if (buySellAsset.Amount > userCryptoBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new(buySellAsset.UserId, buySellAsset.AssetId, $"Vender {crypto.Name}", buySellAsset.Amount, "Crypto");
        user.Cash += transaction.Amount;
        user.Wallet -= transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void BuyStock(BuySellAsset buySellAsset)
    {
        var user = _userRepository.GetUser(buySellAsset.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellAsset.UserId} no encontrado");
        }

        var stock = _stockRepository.GetStock(buySellAsset.AssetId);
        if (stock == null) 
        {
            throw new KeyNotFoundException($"Acción con ID {buySellAsset.AssetId} no encontrada");
        }

        if (user.Cash < buySellAsset.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new(buySellAsset.UserId, buySellAsset.AssetId, $"Comprar {stock.Name}", buySellAsset.Amount, "Stock");
        user.Cash -= transaction.Amount;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellStock(BuySellAsset buySellAsset)
    {
        var user = _userRepository.GetUser(buySellAsset.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellAsset.UserId} no encontrado");
        }

        var stock = _stockRepository.GetStock(buySellAsset.AssetId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {buySellAsset.AssetId} no encontrada");
        } 

        var userHasStock = HasStock(buySellAsset.UserId, buySellAsset.AssetId);
        if (!userHasStock)
        {
            throw new Exception($"El usuario {buySellAsset.UserId} no tiene la acción {buySellAsset.AssetId}");
        }

        var userStockBalance = GetStockBalance(buySellAsset.UserId, buySellAsset.AssetId);
        if (buySellAsset.Amount > userStockBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new(buySellAsset.UserId, buySellAsset.AssetId, $"Vender {stock.Name}", buySellAsset.Amount, "Stock");
        user.Cash += transaction.Amount;
        user.Wallet -= transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void CryptoConverter(CryptoConverterDTO cryptoConverterDTO)
    {
        var user = _userRepository.GetUser(cryptoConverterDTO.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {cryptoConverterDTO.UserId} no encontrado");
        }

        var crypto = _cryptoRepository.GetCrypto(cryptoConverterDTO.CryptoId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoConverterDTO.CryptoId} no encontrada");
        } 

        var newCrypto = _cryptoRepository.GetCrypto(cryptoConverterDTO.NewCryptoId);
        if (newCrypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {cryptoConverterDTO.NewCryptoId} no encontrada");
        } 

        var userHasCrypto = HasCrypto(cryptoConverterDTO.UserId, cryptoConverterDTO.CryptoId);
        if (!userHasCrypto)
        {
            throw new Exception($"El usuario {cryptoConverterDTO.UserId} no tiene la criptomoneda {cryptoConverterDTO.CryptoId}");
        }

        var userCryptoBalance = GetCryptoBalance(cryptoConverterDTO.UserId, cryptoConverterDTO.CryptoId);
        if (cryptoConverterDTO.Amount > userCryptoBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }

        //Transaction transaction = new(userId, cryptoId, $"Convertir {crypto.Name} a {newCrypto.Name}", amount, "Crypto");
        Transaction sellTransaction = new(cryptoConverterDTO.UserId, cryptoConverterDTO.CryptoId, $"Vender {crypto.Name}", cryptoConverterDTO.Amount, "Crypto");
        Transaction buyTransaction = new(cryptoConverterDTO.UserId, cryptoConverterDTO.CryptoId, $"Comprar {newCrypto.Name}", cryptoConverterDTO.Amount, "Crypto");
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(sellTransaction);
        _transactionRepository.AddTransaction(buyTransaction);
    }

    public Dictionary<string, double> MyCryptos(int userId)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }

        var userTransactions = _transactionRepository.GetAllTransactions(userId);
            
        var cryptos = _cryptoRepository.GetAllCryptos().ToDictionary(c => c.Id, c => c.Name);

        var totalAmountByCrypto = new Dictionary<string, double>();

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId.HasValue && transaction.TypeOfAsset.Equals("Crypto"))
            {
                var cryptoName = cryptos[transaction.AssetId.Value];

                if (!totalAmountByCrypto.ContainsKey(cryptoName))
                {
                    totalAmountByCrypto[cryptoName] = 0;
                }
                if (transaction.Concept.StartsWith("Comprar"))
                {
                    totalAmountByCrypto[cryptoName] += transaction.Amount;
                }
                if (transaction.Concept.StartsWith("Vender"))
                {
                    totalAmountByCrypto[cryptoName] -= transaction.Amount;
                }  
            }
        }
        return totalAmountByCrypto;
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

    public bool HasStock(int userId, int stockId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId);

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId == stockId && transaction.TypeOfAsset.Equals("Stock"))
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

    public double GetStockBalance(int userId, int stockId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId);

        double balance = 0;

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId == stockId && transaction.TypeOfAsset.Equals("Stock"))
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