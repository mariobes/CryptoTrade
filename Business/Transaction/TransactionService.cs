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

    public void MakeDeposit(DepositDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        }

        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            Concept = "+",
            Amount = dto.Amount,
            PaymentMethod = dto.PaymentMethod
        };

        user.Cash += dto.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void MakeWithdrawal(WithdrawalDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        }

        if (user.Cash < dto.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar el retiro");
        }

        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            Concept = "-",
            Amount = dto.Amount,
            PaymentMethod = EnumPaymentMethodOptions.TransferenciaBancaria
        };
        user.Cash -= dto.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void BuyCrypto(BuySellAssetDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        }

        var crypto = _cryptoRepository.GetCrypto(dto.AssetId);
        if (crypto == null) 
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {dto.AssetId} no encontrada");
        }

        if (user.Cash < dto.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            Concept = $"+ {crypto.Name}",
            Amount = dto.Amount,
            TypeOfAsset = "Crypto"
        };
        user.Cash -= transaction.Amount;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellCrypto(BuySellAssetDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        }

        var crypto = _cryptoRepository.GetCrypto(dto.AssetId);
        if (crypto == null)
        {
            throw new KeyNotFoundException($"Criptomoneda con ID {dto.AssetId} no encontrada");
        } 

        var userHasCrypto = HasCrypto(dto.UserId, dto.AssetId);
        if (!userHasCrypto)
        {
            throw new Exception($"El usuario {dto.UserId} no tiene la criptomoneda {dto.AssetId}");
        }

        var userCryptoBalance = GetCryptoBalance(dto.UserId, dto.AssetId);
        if (dto.Amount > userCryptoBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            Concept = $"- {crypto.Name}",
            Amount = dto.Amount,
            TypeOfAsset = "Crypto"
        };
        user.Cash += transaction.Amount;
        user.Wallet -= transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void BuyStock(BuySellAssetDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        }

        var stock = _stockRepository.GetStock(dto.AssetId);
        if (stock == null) 
        {
            throw new KeyNotFoundException($"Acción con ID {dto.AssetId} no encontrada");
        }

        if (user.Cash < dto.Amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            Concept = $"+ {stock.Name}",
            Amount = dto.Amount,
            TypeOfAsset = "Stock"
        };
        user.Cash -= transaction.Amount;
        user.Wallet += transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellStock(BuySellAssetDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId);
        if (user == null) 
        {
            throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        }

        var stock = _stockRepository.GetStock(dto.AssetId);
        if (stock == null)
        {
            throw new KeyNotFoundException($"Acción con ID {dto.AssetId} no encontrada");
        } 

        var userHasStock = HasStock(dto.UserId, dto.AssetId);
        if (!userHasStock)
        {
            throw new Exception($"El usuario {dto.UserId} no tiene la acción {dto.AssetId}");
        }

        var userStockBalance = GetStockBalance(dto.UserId, dto.AssetId);
        if (dto.Amount > userStockBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            Concept = $"- {stock.Name}",
            Amount = dto.Amount,
            TypeOfAsset = "Stock"
        };
        user.Cash += transaction.Amount;
        user.Wallet -= transaction.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
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
            if (transaction.AssetId != null && transaction.TypeOfAsset.Equals("Crypto"))
            {
                var cryptoName = cryptos[transaction.AssetId];

                if (!totalAmountByCrypto.ContainsKey(cryptoName))
                {
                    totalAmountByCrypto[cryptoName] = 0;
                }
                if (transaction.Concept.StartsWith("+"))
                {
                    totalAmountByCrypto[cryptoName] += transaction.Amount;
                }
                if (transaction.Concept.StartsWith("-"))
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
            if (transaction.AssetId != null && transaction.TypeOfAsset.Equals("Stock"))
            {
                var stockName = stocks[transaction.AssetId];

                if (!totalAmountByStock.ContainsKey(stockName))
                {
                    totalAmountByStock[stockName] = 0;
                }
                if (transaction.Concept.StartsWith("+"))
                {
                    totalAmountByStock[stockName] += transaction.Amount;
                }
                if (transaction.Concept.StartsWith("-"))
                {
                    totalAmountByStock[stockName] -= transaction.Amount;
                }  
            }
        }
        return totalAmountByStock;
    }

    public bool HasCrypto(int userId, string cryptoId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId);

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId.Equals(cryptoId) && transaction.TypeOfAsset.Equals("Crypto"))
            {
                return true;
            }
        }
        return false;
    }

    public bool HasStock(int userId, string stockId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId);

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId.Equals(stockId) && transaction.TypeOfAsset.Equals("Stock"))
            {
                return true;
            }
        }
        return false;
    }

    public double GetCryptoBalance(int userId, string cryptoId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId);

        double balance = 0;

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId.Equals(cryptoId) && transaction.TypeOfAsset.Equals("Crypto"))
            {
                if (transaction.Concept.StartsWith("+"))
                {
                    balance += transaction.Amount;
                }
                else if (transaction.Concept.StartsWith("-"))
                {
                    balance -= transaction.Amount;
                }
            }
        }
        return balance;
    }

    public double GetStockBalance(int userId, string stockId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId);

        double balance = 0;

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId.Equals(stockId) && transaction.TypeOfAsset.Equals("Stock"))
            {
                if (transaction.Concept.StartsWith("+"))
                {
                    balance += transaction.Amount;
                }
                else if (transaction.Concept.StartsWith("-"))
                {
                    balance -= transaction.Amount;
                }
            }
        }
        return balance;
    }
    
}