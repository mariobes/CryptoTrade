using Microsoft.EntityFrameworkCore;
using CryptoTrade.Models;
using Microsoft.Extensions.Logging;

namespace CryptoTrade.Data
{
    public class CryptoTradeContext : DbContext
    {

        public CryptoTradeContext(DbContextOptions<CryptoTradeContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Email = "admin@cryptotrade.com", Password = "admin12345", Phone = "0000", DNI = "ADMIN", Nationality = "ADMIN", Cash = 10000, Wallet = 0, Role = Roles.Admin},
                new User { Id = 2, Name = "Mario", Birthdate = new DateTime(2003, 3, 3), Email = "mario@gmail.com", Password = "mario12345", Phone = "4567477", DNI = "23523562D", Nationality = "Argentina", Cash = 400, Wallet = 750 },
                new User { Id = 3, Name = "Fernando", Birthdate = new DateTime(2003, 3, 3), Email = "fernando@gmail.com", Password = "fernando12345", Phone = "4745", DNI = "23526445X", Nationality = "España", Cash = 300, Wallet = 1650 },
                new User { Id = 4, Name = "Eduardo", Birthdate = new DateTime(2004, 4, 4), Email = "eduardo@gmail.com", Password = "eduardo12345", Phone = "4574548", DNI = "52353425D", Nationality = "España", Cash = 200, Wallet = 1020 }
            );

            // modelBuilder.Entity<Crypto>().HasData(
            //     new Crypto { Id = 1, Name = "Bitcoin", Symbol = "BTC", MarketCap = 900000000000, CreationDate = new DateTime(2009, 1, 3), Description = "La primera criptomoneda descentralizada", Value = 60000, Website = "https://bitcoin.org", TotalSupply = 21000000, CirculatingSupply = 19000000, Contract = "0x0000000000000000000000000000000000000000", AllTimeHigh = 69000, AllTimeLow = 67 },
            //     new Crypto { Id = 2, Name = "Ethereum", Symbol = "ETH", MarketCap = 400000000000, CreationDate = new DateTime(2015, 7, 30), Description = "Una plataforma descentralizada de contratos inteligentes", Value = 4000, Website = "https://ethereum.org", TotalSupply = 118000000, CirculatingSupply = 118000000, Contract = "0x2170ed0880ac9a755fd29b2688956bd959f933f8", AllTimeHigh = 4800, AllTimeLow = 0.42 },
            //     new Crypto { Id = 3, Name = "Cardano", Symbol = "ADA", MarketCap = 70000000000, CreationDate = new DateTime(2017, 9, 29), Description = "Plataforma blockchain de tercera generación", Value = 2.15, Website = "https://cardano.org", TotalSupply = 45000000000, CirculatingSupply = 32000000000, Contract = "0x3ee2200efb3400fabb9aacf31297cbdd1d435d47", AllTimeHigh = 3.10, AllTimeLow = 0.017 }
            // );

            // modelBuilder.Entity<Stock>().HasData(
            //     new Stock { Id = 1, Name = "Apple Inc.", Value = 150.25, CreationDate = new DateTime(1976, 4, 1), Description = "Empresa multinacional de tecnología", Website = "https://www.apple.com", CompanyValue = 2500000000000, EarningPerShare = 5.61, Category = "Technology", DividendYield = 0.61 },
            //     new Stock { Id = 2, Name = "Microsoft Corporation", Value = 300.55, CreationDate = new DateTime(1975, 4, 4), Description = "Empresa de software y hardware", Website = "https://www.microsoft.com", CompanyValue = 2300000000000, EarningPerShare = 9.78, Category = "Technology", DividendYield = 0.87 },
            //     new Stock { Id = 3, Name = "Tesla Inc.", Value = 750.75, CreationDate = new DateTime(2003, 7, 1), Description = "Empresa de automóviles eléctricos y energías limpias", Website = "https://www.tesla.com", CompanyValue = 800000000000, EarningPerShare = 2.16, Category = "Automotive", DividendYield = 0.00 }
            // );

            // modelBuilder.Entity<Transaction>().HasData(
            //     new Transaction { Id = 1, UserId = 2, Concept = "Ingresar dinero", Amount = 1200, Date = new DateTime(2023, 6, 12), PaymentMethod = EnumPaymentMethodOptions.TransferenciaBancaria },
            //     new Transaction { Id = 2, UserId = 3, Concept = "Ingresar dinero", Amount = 2000, Date = new DateTime(2023, 6, 12), PaymentMethod = EnumPaymentMethodOptions.TarjetaCredito },
            //     new Transaction { Id = 3, UserId = 4, Concept = "Ingresar dinero", Amount = 1000, Date = new DateTime(2023, 6, 12), PaymentMethod = EnumPaymentMethodOptions.GooglePay },
            //     new Transaction { Id = 4, UserId = 2, AssetId = 1, Concept = "Comprar Bitcoin", Amount = 100, PurchasePrice = 60000, AssetAmount = 0.00167, Date = new DateTime(2023, 6, 12), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 5, UserId = 2, AssetId = 2, Concept = "Comprar Ethereum", Amount = 200, PurchasePrice = 4000, AssetAmount = 0.05, Date = new DateTime(2023, 8, 5), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 6, UserId = 2, AssetId = 3, Concept = "Comprar Cardano", Amount = 500, PurchasePrice = 2.15, AssetAmount = 232.56, Date = new DateTime(2023, 9, 10), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 7, UserId = 2, AssetId = 1, Concept = "Vender Bitcoin", Amount = 50, PurchasePrice = 60000, AssetAmount = 0.00083, Date = new DateTime(2023, 10, 2), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 8, UserId = 2, AssetId = 1, Concept = "Comprar Bitcoin", Amount = 100, PurchasePrice = 60000, AssetAmount = 0.00167, Date = new DateTime(2023, 11, 15), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 9, UserId = 3, AssetId = 2, Concept = "Comprar Ethereum", Amount = 50, PurchasePrice = 4000, AssetAmount = 0.0125, Date = new DateTime(2023, 8, 20), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 10, UserId = 3, AssetId = 3, Concept = "Comprar Cardano", Amount = 1000, PurchasePrice = 2.15, AssetAmount = 465.11, Date = new DateTime(2023, 9, 30), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 11, UserId = 3, AssetId = 3, Concept = "Vender Cardano", Amount = 100, PurchasePrice = 2.15, AssetAmount = 46.51, Date = new DateTime(2023, 10, 21), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 12, UserId = 4, AssetId = 2, Concept = "Comprar Ethereum", Amount = 200, PurchasePrice = 60000, AssetAmount = 0.005, Date = new DateTime(2023, 6, 25), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 13, UserId = 4, AssetId = 2, Concept = "Vender Ethereum", Amount = 20, PurchasePrice = 4000, AssetAmount = 0.005, Date = new DateTime(2023, 7, 14), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 14, UserId = 4, AssetId = 1, Concept = "Comprar Bitcoin", Amount = 500, PurchasePrice = 60000, AssetAmount = 0.00833, Date = new DateTime(2023, 10, 6), TypeOfAsset = "Crypto" },
            //     new Transaction { Id = 15, UserId = 2, AssetId = 1, Concept = "Comprar Apple Inc.", Amount = 100, PurchasePrice = 150.25, AssetAmount = 0.665, Date = new DateTime(2023, 9, 7), TypeOfAsset = "Stock" },
            //     new Transaction { Id = 16, UserId = 3, AssetId = 2, Concept = "Comprar Microsoft Corporation", Amount = 500, PurchasePrice = 300.55, AssetAmount = 1.664, Date = new DateTime(2023, 8, 16), TypeOfAsset = "Stock" },
            //     new Transaction { Id = 17, UserId = 4, AssetId = 3, Concept = "Vender Tesla Inc.", Amount = 200, PurchasePrice = 750.75, AssetAmount = 0.133, Date = new DateTime(2023, 9, 18), TypeOfAsset = "Stock" }
            // );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
    }
}
