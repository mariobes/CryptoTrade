using Microsoft.EntityFrameworkCore;
using CryptoTrade.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace CryptoTrade.Data;

public class CryptoTradeContext : DbContext
{
    public CryptoTradeContext(DbContextOptions<CryptoTradeContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Crypto> Cryptos { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<CryptoIndex> CryptoIndices { get; set; }
    public DbSet<CryptoTrending> CryptoTrendings { get; set; }
    public DbSet<StockTrending> StockTrendings { get; set; }
    public DbSet<StockGainer> StockGainers { get; set; }
    public DbSet<StockLoser> StockLosers { get; set; }
    public DbSet<StockMostActive> StockMostActives { get; set; }
    public DbSet<Watchlist> Watchlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Admin", Birthdate = new DateTime(0001, 01, 01), Email = "admin@cryptotrade.com", Password = "YHrp/ExR53lRO6ouA2tT0y9QCb94jfjNBsxcGq5x798=", Phone = "000", DNI = "Admin", Nationality = "Admin", CreationDate = new DateTime(2016, 01, 01), Role = Roles.Admin},
            new User { Id = 2, Name = "Mario", Birthdate = new DateTime(2001, 09, 17), Email = "mario@gmail.com", Password = "JApd9lfG2wshq3agTXjgwVT/f4jQecLCYTBnBT30AqE=", Phone = "567935418", DNI = "25463652D", Nationality = "España", Cash = 384.35, Wallet = 4615.65, CreationDate = new DateTime(2022, 07, 02) },
            new User { Id = 3, Name = "Fernando", Birthdate = new DateTime(2003, 01, 28), Email = "fernando@gmail.com", Password = "xf0cyil3yRNj5rC2KE+3O+wmt/rGtUapwYkq5YfkqG4=", Phone = "541298637", DNI = "26587463X", Nationality = "España", CreationDate = new DateTime(2023, 11, 12) },
            new User { Id = 4, Name = "Eduardo", Birthdate = new DateTime(1998, 11, 04), Email = "eduardo@gmail.com", Password = "6GGegrjO3tQMHPZBrkdANTfPC92ka20ChXH9VdvhLak=", Phone = "658248974", DNI = "52684659D", Nationality = "España", CreationDate = new DateTime(2024, 01, 18) }
        );

        modelBuilder.Entity<Watchlist>().HasData(
            new Watchlist { Id = 1, UserId = 2, AssetId = "bitcoin", TypeAsset = "Crypto" },
            new Watchlist { Id = 2, UserId = 2, AssetId = "ethereum", TypeAsset = "Crypto" },
            new Watchlist { Id = 3, UserId = 2, AssetId = "ripple", TypeAsset = "Crypto" },
            new Watchlist { Id = 4, UserId = 2, AssetId = "cardano", TypeAsset = "Crypto" },
            new Watchlist { Id = 5, UserId = 2, AssetId = "aapl", TypeAsset = "Stock" },
            new Watchlist { Id = 6, UserId = 2, AssetId = "amzn", TypeAsset = "Stock" }
        );

        modelBuilder.Entity<Transaction>().HasData(
            new Transaction { Id = 1, UserId = 2, Concept = "+ Deposit", Amount = 5000, Date = new DateTime(2024, 1, 4), PaymentMethod = EnumPaymentMethodOptions.CreditCard },
            new Transaction { Id = 2, UserId = 2, AssetId = "bitcoin", Concept = "+ Bitcoin", Amount = 109.44, PurchasePrice = 22436.13, AssetAmount = 0.004878, Date = new DateTime(2024, 1, 9), TypeOfAsset = "Crypto" },
            new Transaction { Id = 3, UserId = 2, AssetId = "bitcoin", Concept = "+ Bitcoin", Amount = 218.89, PurchasePrice = 28455.57, AssetAmount = 0.007692, Date = new DateTime(2024, 1, 9), TypeOfAsset = "Crypto" },
            new Transaction { Id = 4, UserId = 2, AssetId = "bitcoin", Concept = "+ Bitcoin", Amount = 54.72, PurchasePrice = 45749.96, AssetAmount = 0.001196, Date = new DateTime(2024, 1, 9), TypeOfAsset = "Crypto" },
            new Transaction { Id = 5, UserId = 2, AssetId = "bitcoin", Concept = "+ Bitcoin", Amount = 54.72, PurchasePrice = 42904.27, AssetAmount = 0.001276, Date = new DateTime(2024, 1, 9), TypeOfAsset = "Crypto" },
            new Transaction { Id = 6, UserId = 2, AssetId = "bitcoin", Concept = "+ Bitcoin", Amount = 109.46, PurchasePrice = 43891.48, AssetAmount = 0.002494, Date = new DateTime(2024, 1, 9), TypeOfAsset = "Crypto" },
            new Transaction { Id = 7, UserId = 2, AssetId = "hedera-hashgraph", Concept = "+ Hedera", Amount = 654.37, PurchasePrice = 0.08926, AssetAmount = 7331.13, Date = new DateTime(2024, 2, 24), TypeOfAsset = "Crypto" },
            new Transaction { Id = 8, UserId = 2, AssetId = "cardano", Concept = "+ Cardano", Amount = 529.36, PurchasePrice = 0.4514, AssetAmount = 1172.51, Date = new DateTime(2024, 2, 24), TypeOfAsset = "Crypto" },
            new Transaction { Id = 9, UserId = 2, AssetId = "ripple", Concept = "+ XRP", Amount = 322.91, PurchasePrice = 0.6045, AssetAmount = 534.22, Date = new DateTime(2024, 2, 24), TypeOfAsset = "Crypto" },
            new Transaction { Id = 10, UserId = 2, AssetId = "ondo-finance", Concept = "+ Ondo", Amount = 203.20, PurchasePrice = 0.7726, AssetAmount = 263.01, Date = new DateTime(2024, 5, 13), TypeOfAsset = "Crypto" },
            new Transaction { Id = 11, UserId = 2, AssetId = "ethereum", Concept = "+ Ethereum", Amount = 507.43, PurchasePrice = 2332.92, AssetAmount = 0.2175, Date = new DateTime(2024, 8, 5), TypeOfAsset = "Crypto" },
            new Transaction { Id = 12, UserId = 2, AssetId = "polkadot", Concept = "+ Polkadot", Amount = 229.09, PurchasePrice = 6.4170, AssetAmount = 35.70, Date = new DateTime(2024, 8, 6), TypeOfAsset = "Crypto" },
            new Transaction { Id = 13, UserId = 2, AssetId = "polkadot", Concept = "+ Polkadot", Amount = 195.47, PurchasePrice = 6.4170, AssetAmount = 30.46, Date = new DateTime(2024, 8, 6), TypeOfAsset = "Crypto" },
            new Transaction { Id = 14, UserId = 2, AssetId = "pepe", Concept = "+ Pepe", Amount = 157.43, PurchasePrice = 0.00000672, AssetAmount = 23400000, Date = new DateTime(2024, 8, 6), TypeOfAsset = "Crypto" },
            new Transaction { Id = 15, UserId = 2, AssetId = "near", Concept = "+ NEAR Protocol", Amount = 200.01, PurchasePrice = 7.7211, AssetAmount = 25.90, Date = new DateTime(2024, 12, 8), TypeOfAsset = "Crypto" },
            new Transaction { Id = 16, UserId = 2, AssetId = "near", Concept = "+ NEAR Protocol", Amount = 309.94, PurchasePrice = 4.9240, AssetAmount = 62.94, Date = new DateTime(2025, 1, 15), TypeOfAsset = "Crypto" },
            new Transaction { Id = 17, UserId = 2, AssetId = "render-token", Concept = "+ Render", Amount = 309.32, PurchasePrice = 6.8830, AssetAmount = 44.94, Date = new DateTime(2025, 1, 15), TypeOfAsset = "Crypto" },
            new Transaction { Id = 18, UserId = 2, AssetId = "render-token", Concept = "+ Render", Amount = 99.89, PurchasePrice = 4.1640, AssetAmount = 23.99, Date = new DateTime(2025, 2, 7), TypeOfAsset = "Crypto" },
            new Transaction { Id = 19, UserId = 2, AssetId = "msft", Concept = "+ Microsoft Corporation", Amount = 100, PurchasePrice = 320, AssetAmount = 0.3125, Date = new DateTime(2025, 4, 20), TypeOfAsset = "Stock" },
            new Transaction { Id = 20, UserId = 2, AssetId = "nvda", Concept = "+ NVIDIA Corporation", Amount = 250, PurchasePrice = 120, AssetAmount = 2.083, Date = new DateTime(2025, 4, 25), TypeOfAsset = "Stock" }
        );

        modelBuilder.Entity<Crypto>(entity =>
        {
            entity.Property(e => e.SparklineIn7d)
                .HasConversion(
                    v => v != null && v.Price != null && v.Price.Any()
                        ? string.Join(",", v.Price.Select(price => price.ToString("0.000000000000", CultureInfo.InvariantCulture)))
                        : string.Empty,
                    v => 
                        new SparklineIn7d
                        {
                            Price = string.IsNullOrEmpty(v)
                                ? new List<double>()  // Si es vacío, retornar una lista vacía
                                : ConvertToDoubleList(v)  // Convertir la cadena de nuevo a una lista
                        }
                );
        });
    }

    private List<double> ConvertToDoubleList(string v)
    {
        var priceStrings = v.Split(',');
        var prices = new List<double>();

        foreach (var price in priceStrings)
        {
            if (double.TryParse(price.Trim(), out var parsedPrice))
            {
                prices.Add(parsedPrice);
            }
        }

        return prices;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();
    }
}