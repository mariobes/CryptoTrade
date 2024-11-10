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

    public void MakeDeposit(DepositDTO depositDTO)
    {
        var user = _userRepository.GetUser(depositDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {depositDTO.UserId} no encontrado");
        }

        Transaction transaction = new(depositDTO.UserId, "Ingresar dinero", depositDTO.Amount, depositDTO.PaymentMethod);
        user.Cash += depositDTO.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void MakeWithdrawal(WithdrawalDTO withdrawalDTO)
    {
        var user = _userRepository.GetUser(withdrawalDTO.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {withdrawalDTO.UserId} no encontrado");
        }

        if (user.Cash < withdrawalDTO.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar el retiro");
        }

        Transaction transaction = new(withdrawalDTO.UserId, "Retirar dinero", withdrawalDTO.Amount, PaymentMethodOptions.TransferenciaBancaria);
        user.Cash -= withdrawalDTO.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void BuyCrypto(BuySellAssetDTO buySellAssetDTO)
    {
        var user = _userRepository.GetUser(buySellAssetDTO.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellAssetDTO.UserId} no encontrado");
        }

        var crypto = _cryptoRepository.GetCrypto(buySellAssetDTO.AssetId);
        if (crypto == null) 
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {buySellAssetDTO.AssetId} no encontrada");
        }

        if (user.Cash < buySellAssetDTO.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new(buySellAssetDTO.UserId, buySellAssetDTO.AssetId, $"Comprar {crypto.Name}", buySellAssetDTO.Amount, "Crypto");
        user.Cash -= transaction.Amount;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellCrypto(BuySellAssetDTO buySellAssetDTO)
    {
        var user = _userRepository.GetUser(buySellAssetDTO.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellAssetDTO.UserId} no encontrado");
        }

        var crypto = _cryptoRepository.GetCrypto(buySellAssetDTO.AssetId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {buySellAssetDTO.AssetId} no encontrada");
        } 

        var userHasCrypto = HasCrypto(buySellAssetDTO.UserId, buySellAssetDTO.AssetId);
        if (!userHasCrypto)
        {
            throw new Exception($"El usuario {buySellAssetDTO.UserId} no tiene la criptomoneda {buySellAssetDTO.AssetId}");
        }

        var userCryptoBalance = GetCryptoBalance(buySellAssetDTO.UserId, buySellAssetDTO.AssetId);
        if (buySellAssetDTO.Amount > userCryptoBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new(buySellAssetDTO.UserId, buySellAssetDTO.AssetId, $"Vender {crypto.Name}", buySellAssetDTO.Amount, "Crypto");
        user.Cash += transaction.Amount;
        user.Wallet -= transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void BuyStock(BuySellAssetDTO buySellAssetDTO)
    {
        var user = _userRepository.GetUser(buySellAssetDTO.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellAssetDTO.UserId} no encontrado");
        }

        var stock = _stockRepository.GetStock(buySellAssetDTO.AssetId);
        if (stock == null) 
        {
            throw new KeyNotFoundException($"Acción con ID {buySellAssetDTO.AssetId} no encontrada");
        }

        if (user.Cash < buySellAssetDTO.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new(buySellAssetDTO.UserId, buySellAssetDTO.AssetId, $"Comprar {stock.Name}", buySellAssetDTO.Amount, "Stock");
        user.Cash -= transaction.Amount;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellStock(BuySellAssetDTO buySellAssetDTO)
    {
        var user = _userRepository.GetUser(buySellAssetDTO.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {buySellAssetDTO.UserId} no encontrado");
        }

        var stock = _stockRepository.GetStock(buySellAssetDTO.AssetId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {buySellAssetDTO.AssetId} no encontrada");
        } 

        var userHasStock = HasStock(buySellAssetDTO.UserId, buySellAssetDTO.AssetId);
        if (!userHasStock)
        {
            throw new Exception($"El usuario {buySellAssetDTO.UserId} no tiene la acción {buySellAssetDTO.AssetId}");
        }

        var userStockBalance = GetStockBalance(buySellAssetDTO.UserId, buySellAssetDTO.AssetId);
        if (buySellAssetDTO.Amount > userStockBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new(buySellAssetDTO.UserId, buySellAssetDTO.AssetId, $"Vender {stock.Name}", buySellAssetDTO.Amount, "Stock");
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

    public Dictionary<string, double> MyStocks(int userId)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }

        var userTransactions = _transactionRepository.GetAllTransactions(userId);
            
        var stocks = _stockRepository.GetAllStocks().ToDictionary(s => s.Id, s => s.Name);

        var totalAmountByStock = new Dictionary<string, double>();

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId.HasValue && transaction.TypeOfAsset.Equals("Stock"))
            {
                var stockName = stocks[transaction.AssetId.Value];

                if (!totalAmountByStock.ContainsKey(stockName))
                {
                    totalAmountByStock[stockName] = 0;
                }
                if (transaction.Concept.StartsWith("Comprar"))
                {
                    totalAmountByStock[stockName] += transaction.Amount;
                }
                if (transaction.Concept.StartsWith("Vender"))
                {
                    totalAmountByStock[stockName] -= transaction.Amount;
                }  
            }
        }
        return totalAmountByStock;
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