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

    public IEnumerable<UserAssetsSummaryDto> MyAssets(int userId, string? assetType = null, string? assetId = null)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }

        var transactions = _transactionRepository.GetAllTransactions(userId)
            .Where(t => t.AssetId != null && (assetType == null || t.TypeOfAsset == assetType));

        if (!string.IsNullOrEmpty(assetId))
        {
            transactions = transactions.Where(t => t.AssetId == assetId);
        }

        var cryptoDict = _cryptoRepository.GetAllCryptos().ToDictionary(c => c.Id, c => new
        {
            c.Name,
            Price = c.Price ?? 0,
            c.Symbol,
            c.Image,
            ChangePercentage24h = c.PriceChangePercentage24h ?? 0,
            c.LastUpdated
        });

        var stockDict = _stockRepository.GetAllStocks().ToDictionary(s => s.Id, s => new
        {
            s.Name,
            Price = s.Price ?? 0,
            s.Symbol,
            s.Image,
            ChangePercentage24h = s.ChangesPercentage ?? 0,
            s.LastUpdated
        });

        var grouped = transactions.GroupBy(t => t.AssetId);
        var result = new List<UserAssetsSummaryDto>();
        double totalBalance = 0;

        foreach (var group in grouped)
        {
            var assetIdGroup = group.Key!;
            var firstTransaction = group.First();
            var isCrypto = firstTransaction.TypeOfAsset == "Crypto";
            var assetDict = isCrypto ? cryptoDict : stockDict;

            if (!assetDict.ContainsKey(assetIdGroup)) continue;

            var asset = assetDict[assetIdGroup];
            double totalAssetAmount = 0;
            double totalInvestedAmount = 0;
            double avgPurchasePrice = 0;
            double realizedProfit = 0;

            foreach (var transaction in group.OrderBy(t => t.Date))
            {
                var assetAmount = transaction.AssetAmount ?? 0;
                var amount = transaction.Amount;

                if (transaction.Concept.StartsWith("+"))
                {
                    totalAssetAmount += assetAmount;
                    totalInvestedAmount += amount;
                    avgPurchasePrice = totalAssetAmount != 0 ? totalInvestedAmount / totalAssetAmount : 0;
                }
                else if (transaction.Concept.StartsWith("-"))
                {
                    double costBasis = assetAmount * avgPurchasePrice;
                    double gain = amount - costBasis;
                    realizedProfit += gain;

                    totalAssetAmount -= assetAmount;
                    totalInvestedAmount -= costBasis;
                }
            }

            double unrealizedBalance = totalAssetAmount * asset.Price - totalInvestedAmount;
            double totalBalanceWithRealized = unrealizedBalance + realizedProfit;
            double total = totalInvestedAmount + unrealizedBalance;

            totalBalance += totalBalanceWithRealized;

            result.Add(new UserAssetsSummaryDto
            {
                AssetId = assetIdGroup,
                Name = asset.Name,
                TotalInvestedAmount = totalInvestedAmount,
                TotalAssetAmount = totalAssetAmount,
                AveragePurchasePrice = avgPurchasePrice,
                Balance = totalBalanceWithRealized,
                BalancePercentage = (totalAssetAmount > 0 && avgPurchasePrice > 0) ? unrealizedBalance / (totalAssetAmount * avgPurchasePrice) * 100 : 0,
                Total = total,
                WalletPercentage = 0,
                TypeOfAsset = isCrypto ? "Crypto" : "Stock",
                Symbol = asset.Symbol,
                Image = asset.Image,
                Price = asset.Price,
                ChangesPercentage24h = asset.ChangePercentage24h
            });
        }

        double walletBase = user.Wallet + user.Profit;
        foreach (var asset in result)
        {
            asset.WalletPercentage = walletBase != 0 ? asset.Total / walletBase * 100 : 0;
        }

        return result.OrderByDescending(a => a.Total);
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
    
    public void UpdateAllUsersBalances()
    {
        var allUsers = _userRepository.GetAllUsers().ToList();
        var allCryptos = _cryptoRepository.GetAllCryptos().ToList().ToDictionary(c => c.Id, c => c);
        var allStocks = _stockRepository.GetAllStocks().ToList().ToDictionary(s => s.Id, s => s);

        var nowDate = DateTime.UtcNow.AddHours(2).Date;

        foreach (var user in allUsers)
        {
            var transactions = _transactionRepository.GetAllTransactions(user.Id).Where(t => t.AssetId != null).ToList();

            var grouped = transactions.GroupBy(t => t.AssetId);
            double totalBalance = 0;

            foreach (var group in grouped)
            {
                var assetId = group.Key!;
                var firstTransaction = group.First();
                var isCrypto = firstTransaction.TypeOfAsset == "Crypto";

                double assetPrice = 0;

                if (isCrypto)
                {
                    if (!allCryptos.ContainsKey(assetId)) continue;
                    assetPrice = allCryptos[assetId].Price ?? 0;
                }
                else
                {
                    if (!allStocks.ContainsKey(assetId)) continue;
                    assetPrice = allStocks[assetId].Price ?? 0;
                }

                double totalAssetAmount = 0;
                double totalInvestedAmount = 0;
                double avgPurchasePrice = 0;
                double realizedProfit = 0;

                foreach (var transaction in group.OrderBy(t => t.Date))
                {
                    var assetAmount = transaction.AssetAmount ?? 0;
                    var amount = transaction.Amount;

                    if (transaction.Concept.StartsWith("+"))
                    {
                        totalAssetAmount += assetAmount;
                        totalInvestedAmount += amount;
                        avgPurchasePrice = totalAssetAmount != 0 ? totalInvestedAmount / totalAssetAmount : 0;
                    }
                    else if (transaction.Concept.StartsWith("-"))
                    {
                        double costBasis = assetAmount * avgPurchasePrice;
                        double gain = amount - costBasis;
                        realizedProfit += gain;

                        totalAssetAmount -= assetAmount;
                        totalInvestedAmount -= costBasis;
                    }
                }

                double unrealizedBalance = totalAssetAmount * assetPrice - totalInvestedAmount;
                double totalBalanceWithRealized = unrealizedBalance + realizedProfit;
                totalBalance += totalBalanceWithRealized;
            }

            var anyUpdatedToday = allCryptos.Values.Any(c => c.LastUpdated.Date == nowDate) ||
                                allStocks.Values.Any(s => s.LastUpdated.Date == nowDate);

            if (user.LastUpdated.Date != nowDate && anyUpdatedToday)
            {
                user.LastBalance = user.Wallet + user.Profit;
                var newProfit = totalBalance - user.Profit;
                user.Profit = Math.Round(user.Profit + newProfit, 2);
                user.LastUpdated = DateTime.UtcNow.AddHours(2);

                _userRepository.UpdateUser(user);
            }
        }
    }
}