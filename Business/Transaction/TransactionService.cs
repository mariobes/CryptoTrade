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
            Concept = "+ Deposit",
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
            Concept = "- Withdrawal",
            Amount = dto.Amount,
            PaymentMethod = EnumPaymentMethodOptions.BankTransfer
        };
        user.Cash -= dto.Amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void BuyCrypto(BuySellAssetDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId) ?? throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        var crypto = _cryptoRepository.GetCrypto(dto.AssetId) ?? throw new KeyNotFoundException($"Criptomoneda con ID {dto.AssetId} no encontrada");

        double amount, assetAmount;

        if (dto.Amount.HasValue && dto.Amount > 0)
        {
            amount = dto.Amount.Value;
            assetAmount = amount / crypto.Price.Value;
        }
        else if (dto.AssetAmount.HasValue && dto.AssetAmount > 0)
        {
            assetAmount = dto.AssetAmount.Value;
            amount = assetAmount * crypto.Price.Value;
        }
        else
        {
            throw new ArgumentException("Debe especificar Amount o AssetAmount");
        }

        if (user.Cash < amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            Concept = $"+ {crypto.Name}",
            Amount = amount,
            PurchasePrice = crypto.Price.Value,
            AssetAmount = assetAmount,
            TypeOfAsset = "Crypto"
        };

        user.Cash -= amount;
        user.Wallet += amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellCrypto(BuySellAssetDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId) ?? throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        var crypto = _cryptoRepository.GetCrypto(dto.AssetId) ?? throw new KeyNotFoundException($"Criptomoneda con ID {dto.AssetId} no encontrada");

        double amount, assetAmount;

        if (dto.Amount.HasValue && dto.Amount > 0)
        {
            amount = dto.Amount.Value;
            assetAmount = amount / crypto.Price.Value;
        }
        else if (dto.AssetAmount.HasValue && dto.AssetAmount > 0)
        {
            assetAmount = dto.AssetAmount.Value;
            amount = assetAmount * crypto.Price.Value;
        }
        else
        {
            throw new ArgumentException("Debe especificar Amount o AssetAmount");
        }

        var userCryptoBalance = GetCryptoBalance(dto.UserId, dto.AssetId);
        if (assetAmount > userCryptoBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            Concept = $"- {crypto.Name}",
            Amount = amount,
            PurchasePrice = crypto.Price.Value,
            AssetAmount = assetAmount,
            TypeOfAsset = "Crypto"
        };
        user.Cash += amount;
        user.Wallet -= amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void BuyStock(BuySellAssetDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId) ?? throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        var stock = _stockRepository.GetStock(dto.AssetId) ?? throw new KeyNotFoundException($"Acción con ID {dto.AssetId} no encontrada");

        double amount, assetAmount;

        if (dto.Amount.HasValue && dto.Amount > 0)
        {
            amount = dto.Amount.Value;
            assetAmount = amount / stock.Price.Value;
        }
        else if (dto.AssetAmount.HasValue && dto.AssetAmount > 0)
        {
            assetAmount = dto.AssetAmount.Value;
            amount = assetAmount * stock.Price.Value;
        }
        else
        {
            throw new ArgumentException("Debe especificar Amount o AssetAmount");
        }

        if (user.Cash < amount)
        {
            throw new Exception("No tienes suficiente saldo para realizar la compra");
        }

        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            Concept = $"+ {stock.Name}",
            Amount = amount,
            PurchasePrice = stock.Price.Value,
            AssetAmount = assetAmount,
            TypeOfAsset = "Stock"
        };
        user.Cash -= amount;
        user.Wallet += amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public void SellStock(BuySellAssetDto dto)
    {
        var user = _userRepository.GetUser(dto.UserId) ?? throw new KeyNotFoundException($"Usuario con ID {dto.UserId} no encontrado");
        var stock = _stockRepository.GetStock(dto.AssetId) ?? throw new KeyNotFoundException($"Acción con ID {dto.AssetId} no encontrada");

        double amount, assetAmount;

        if (dto.Amount.HasValue && dto.Amount > 0)
        {
            amount = dto.Amount.Value;
            assetAmount = amount / stock.Price.Value;
        }
        else if (dto.AssetAmount.HasValue && dto.AssetAmount > 0)
        {
            assetAmount = dto.AssetAmount.Value;
            amount = assetAmount * stock.Price.Value;
        }
        else
        {
            throw new ArgumentException("Debe especificar Amount o AssetAmount");
        }

        var userStockBalance = GetStockBalance(dto.UserId, dto.AssetId);
        if (assetAmount > userStockBalance)
        {
            throw new Exception($"No tienes suficientes fondos para realizar la venta");
        }
        
        Transaction transaction = new Transaction
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            Concept = $"- {stock.Name}",
            Amount = amount,
            PurchasePrice = stock.Price.Value,
            AssetAmount = assetAmount,
            TypeOfAsset = "Stock"
        };
        user.Cash += amount;
        user.Wallet -= amount;
        _userRepository.UpdateUser(user);
        _transactionRepository.AddTransaction(transaction);
    }

    public IEnumerable<UserAssetsSummaryDto> MyCryptos(int userId, string? cryptoId = null)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }

        var userTransactions = _transactionRepository.GetAllTransactions(userId)
                                                     .Where(t => t.TypeOfAsset == "Crypto" && t.AssetId != null);

        if (!string.IsNullOrEmpty(cryptoId))
        {
            userTransactions = userTransactions.Where(t => t.AssetId == cryptoId);
        }
                
        var cryptos = _cryptoRepository.GetAllCryptos().ToDictionary(c => c.Id, c => new { c.Name, c.Price, c.Symbol, c.Image, c.PriceChangePercentage24h });

        var grouped = userTransactions.GroupBy(t => t.AssetId);

        var result = new List<UserAssetsSummaryDto>();
        double totalPortfolioInvested = 0;
        double totalBalance = 0;

        foreach (var group in grouped)
        {
            foreach (var transaction in group)
            {
                if (transaction.Concept.StartsWith("+"))
                {
                    totalPortfolioInvested += transaction.Amount;
                }
                else if (transaction.Concept.StartsWith("-"))
                {
                    totalPortfolioInvested -= transaction.Amount;
                }
            }
        }

        foreach (var group in grouped)
        {
            var assetId = group.Key;
            var cryptoName = cryptos.ContainsKey(assetId) ? cryptos[assetId].Name : "Unknown";
            double currentPrice = cryptos.ContainsKey(assetId) ? cryptos[assetId].Price ?? 0 : 0;

            double totalAssetAmount = 0;
            double totalInvestedAmount = 0;

            foreach (var transaction in group)
            {
                var assetAmount = transaction.AssetAmount ?? 0;
                var amount = transaction.Amount;

                if (transaction.Concept.StartsWith("+"))
                {
                    totalAssetAmount += assetAmount;
                    totalInvestedAmount += amount;
                }
                else if (transaction.Concept.StartsWith("-"))
                {
                    totalAssetAmount -= assetAmount;
                    totalInvestedAmount -= amount;
                }
            }

            double balance = totalAssetAmount * currentPrice - totalInvestedAmount;
            double total = balance + totalInvestedAmount;

            totalBalance += balance;

            result.Add(new UserAssetsSummaryDto
            {
                AssetId = assetId,
                Name = cryptoName,
                TotalInvestedAmount = totalInvestedAmount,
                TotalAssetAmount = totalAssetAmount,
                AveragePurchasePrice = totalAssetAmount != 0 ? totalInvestedAmount / totalAssetAmount : 0,
                Balance = balance,
                BalancePercentage = totalInvestedAmount != 0 ? balance / totalInvestedAmount * 100 : 0,
                Total = total,
                WalletPercentage = 0,
                TypeOfAsset = "Crypto",
                Symbol = cryptos.ContainsKey(assetId) ? cryptos[assetId].Symbol : string.Empty,
                Image = cryptos.ContainsKey(assetId) ? cryptos[assetId].Image : string.Empty,
                Price = currentPrice,
                ChangesPercentage24h = cryptos.ContainsKey(assetId) ? cryptos[assetId].PriceChangePercentage24h : 0,
            });
        }

        if (string.IsNullOrEmpty(cryptoId))
        {
            if (user.LastUpdated.Date != DateTime.UtcNow.AddHours(2).Date)
            {
                user.LastBalance = user.Wallet + user.Profit;
                var newProfit = totalBalance - user.Profit;
                user.Profit = Math.Round(user.Profit + newProfit, 2);
                user.LastUpdated = DateTime.UtcNow.AddHours(2);
                _userRepository.UpdateUser(user);
            }
        }

        double walletBase = user.Wallet + user.Profit;

        foreach (var crypto in result)
        {
            crypto.WalletPercentage = walletBase != 0 ? crypto.Total / walletBase * 100 : 0;
        }

        return result.OrderByDescending(c => c.Total);
    }

    public IEnumerable<UserAssetsSummaryDto> MyStocks(int userId, string? stockId = null)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }

        var userTransactions = _transactionRepository.GetAllTransactions(userId)
                                                    .Where(t => t.TypeOfAsset == "Stock" && t.AssetId != null);

        if (!string.IsNullOrEmpty(stockId))
        {
            userTransactions = userTransactions.Where(t => t.AssetId == stockId);
        }

        // Obtener todos los stocks y sus precios
        var stocks = _stockRepository.GetAllStocks().ToDictionary(s => s.Id, s => new { s.Name, s.Price, s.Symbol, s.Image, s.ChangesPercentage });

        // Agrupar las transacciones por AssetId
        var grouped = userTransactions.GroupBy(t => t.AssetId);

        var result = new List<UserAssetsSummaryDto>();
        double totalPortfolioInvested = 0;
        double totalBalance = 0;

        // Primero calculamos el total invertido en la cartera
        foreach (var group in grouped)
        {
            foreach (var transaction in group)
            {
                if (transaction.Concept.StartsWith("+"))
                {
                    totalPortfolioInvested += transaction.Amount;
                }
                else if (transaction.Concept.StartsWith("-"))
                {
                    totalPortfolioInvested -= transaction.Amount;
                }
            }
        }

        // Luego recorremos los stocks y sus transacciones
        foreach (var group in grouped)
        {
            var assetId = group.Key;
            var stockName = stocks.ContainsKey(assetId) ? stocks[assetId].Name : "Unknown";

            // Aquí, aseguramos que currentPrice sea un double, usando el operador ?? para asignar 0 si es null
            double currentPrice = stocks.ContainsKey(assetId) ? stocks[assetId].Price ?? 0 : 0;

            double totalAssetAmount = 0;
            double totalInvestedAmount = 0;

            // Calcular la cantidad de stock y la inversión
            foreach (var transaction in group)
            {
                var assetAmount = transaction.AssetAmount ?? 0;
                var amount = transaction.Amount;

                if (transaction.Concept.StartsWith("+"))
                {
                    totalAssetAmount += assetAmount;
                    totalInvestedAmount += amount;
                }
                else if (transaction.Concept.StartsWith("-"))
                {
                    totalAssetAmount -= assetAmount;
                    totalInvestedAmount -= amount;
                }
            }

            double balance = totalAssetAmount * currentPrice - totalInvestedAmount;
            double total = balance + totalInvestedAmount;

            totalBalance += balance;

            result.Add(new UserAssetsSummaryDto
            {
                AssetId = assetId,
                Name = stockName,
                TotalInvestedAmount = totalInvestedAmount,
                TotalAssetAmount = totalAssetAmount,
                AveragePurchasePrice = totalAssetAmount != 0 ? totalInvestedAmount / totalAssetAmount : 0,
                Balance = balance,
                BalancePercentage = totalInvestedAmount != 0 ? balance / totalInvestedAmount * 100 : 0,
                Total = total,
                WalletPercentage = 0,
                TypeOfAsset = "Stock",
                Symbol = stocks.ContainsKey(assetId) ? stocks[assetId].Symbol : string.Empty,
                Image = stocks.ContainsKey(assetId) ? stocks[assetId].Image : string.Empty,
                Price = currentPrice,
                ChangesPercentage24h = stocks.ContainsKey(assetId) ? stocks[assetId].ChangesPercentage : 0,
            });
        }

        if (string.IsNullOrEmpty(stockId))
        {
            if (user.LastUpdated.Date != DateTime.UtcNow.AddHours(2).Date)
            {
                var newProfit = totalBalance - user.Profit;
                user.Profit = Math.Round(user.Profit + newProfit, 2);
                user.LastUpdated = DateTime.UtcNow.AddHours(2);
                _userRepository.UpdateUser(user);
            }
        }

        double walletBase = user.Wallet + user.Profit;

        foreach (var crypto in result)
        {
            crypto.WalletPercentage = walletBase != 0 ? crypto.Total / walletBase * 100 : 0;
        }

        return result.OrderByDescending(s => s.Total);
    }

    public double GetCryptoBalance(int userId, string cryptoId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId)
                                                     .Where(t => !string.IsNullOrEmpty(t.AssetId))
                                                     .ToList();

        double assetBalance = 0;

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId.Equals(cryptoId) && transaction.TypeOfAsset.Equals("Crypto"))
            {
                if (transaction.Concept.StartsWith("+"))
                {
                    assetBalance += transaction.AssetAmount ?? 0;
                }
                else if (transaction.Concept.StartsWith("-"))
                {
                    assetBalance -= transaction.AssetAmount ?? 0;
                }
            }
        }
        return assetBalance;
    }

    public double GetStockBalance(int userId, string stockId)
    {
        var userTransactions = _transactionRepository.GetAllTransactions(userId)
                                                     .Where(t => !string.IsNullOrEmpty(t.AssetId))
                                                     .ToList();

        double assetBalance = 0;

        foreach (var transaction in userTransactions)
        {
            if (transaction.AssetId.Equals(stockId) && transaction.TypeOfAsset.Equals("Stock"))
            {
                if (transaction.Concept.StartsWith("+"))
                {
                    assetBalance += transaction.AssetAmount ?? 0;
                }
                else if (transaction.Concept.StartsWith("-"))
                {
                    assetBalance -= transaction.AssetAmount ?? 0;
                }
            }
        }
        return assetBalance;
    }
    
}