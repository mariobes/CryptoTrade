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
                new User { Id = 1, Name = "Mario", Birthdate = new DateTime(2001, 1, 1), Email = "mario@gmail.com", Password = "mario12345", Phone = "4574", DNI = "32452464D", Nationality = "España", Cash = 0, Wallet = 0, Role = Roles.Admin},
                new User { Id = 2, Name = "Carlos", Birthdate = new DateTime(2003, 3, 3), Email = "carlos@gmail.com", Password = "carlos12345", Phone = "4567477", DNI = "23523562D", Nationality = "Argentina", Cash = 146, Wallet = 350 },
                new User { Id = 3, Name = "Fernando", Birthdate = new DateTime(2003, 3, 3), Email = "fernando@gmail.com", Password = "fernando12345", Phone = "4745", DNI = "23526445X", Nationality = "España", Cash = 0, Wallet = 0 },
                new User { Id = 4, Name = "Eduardo", Birthdate = new DateTime(2004, 4, 4), Email = "eduardo@gmail.com", Password = "eduardo12345", Phone = "4574548", DNI = "52353425D", Nationality = "España", Cash = 0, Wallet = 0 }
            );

            modelBuilder.Entity<Crypto>().HasData(
                new Crypto { Id = 1, Name = "Bitcoin", Symbol = "BTC", MarketCap = "900B", CreationDate = new DateTime(2009, 1, 3), Description = "La primera criptomoneda descentralizada", Value = 60000, Ranking = 1, Website = "https://bitcoin.org", TotalSupply = 21000000, CirculatingSupply = 19000000, Contract = "0x0000000000000000000000000000000000000000", AllTimeHigh = 69000, AllTimeLow = 67 },
                new Crypto { Id = 2, Name = "Ethereum", Symbol = "ETH", MarketCap = "400B", CreationDate = new DateTime(2015, 7, 30), Description = "Una plataforma descentralizada de contratos inteligentes", Value = 4000, Ranking = 2, Website = "https://ethereum.org", TotalSupply = 118000000, CirculatingSupply = 118000000, Contract = "0x2170ed0880ac9a755fd29b2688956bd959f933f8", AllTimeHigh = 4800, AllTimeLow = 0.42 },
                new Crypto { Id = 3, Name = "Cardano", Symbol = "ADA", MarketCap = "70B", CreationDate = new DateTime(2017, 9, 29), Description = "Plataforma blockchain de tercera generación", Value = 2.15, Ranking = 5, Website = "https://cardano.org", TotalSupply = 45000000000, CirculatingSupply = 32000000000, Contract = "0x3ee2200efb3400fabb9aacf31297cbdd1d435d47", AllTimeHigh = 3.10, AllTimeLow = 0.017 }
            );

            modelBuilder.Entity<Stock>().HasData(
                new Stock { Id = 1, Name = "Apple Inc.", Value = 150.25, CreationDate = new DateTime(1976, 4, 1), Description = "Empresa multinacional de tecnología", Ranking = 1, Website = "https://www.apple.com", CompanyValue = 2500000000000, EarningPerShare = 5.61, Category = "Technology", DividendYield = 0.61 },
                new Stock { Id = 2, Name = "Microsoft Corporation", Value = 300.55, CreationDate = new DateTime(1975, 4, 4), Description = "Empresa de software y hardware", Ranking = 2, Website = "https://www.microsoft.com", CompanyValue = 2300000000000, EarningPerShare = 9.78, Category = "Technology", DividendYield = 0.87 },
                new Stock { Id = 3, Name = "Tesla, Inc.", Value = 750.75, CreationDate = new DateTime(2003, 7, 1), Description = "Empresa de automóviles eléctricos y energías limpias", Ranking = 3, Website = "https://www.tesla.com", CompanyValue = 800000000000, EarningPerShare = 2.16, Category = "Automotive", DividendYield = 0.00 }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, UserId = 1, AssetId = 1, Concept = "Compra de Bitcoin", Amount = 0.1, PaymentMethod = "Tarjeta de Crédito", Date = new DateTime(2023, 6, 12) },
                new Transaction { Id = 2, UserId = 1, AssetId = 2, Concept = "Compra de Ethereum", Amount = 1.5, PaymentMethod = "Transferencia Bancaria", Date = new DateTime(2023, 8, 5) },
                new Transaction { Id = 3, UserId = 2, AssetId = 3, Concept = "Compra de Cardano", Amount = 500, PaymentMethod = "Tarjeta de Crédito", Date = new DateTime(2023, 9, 10) },
                new Transaction { Id = 4, UserId = 2, AssetId = 1, Concept = "Venta de Bitcoin", Amount = 0.05, PaymentMethod = "Wallet", Date = new DateTime(2023, 10, 2) },
                new Transaction { Id = 5, UserId = 2, AssetId = 1, Concept = "Compra de Bitcoin", Amount = 0.3, PaymentMethod = "Transferencia Bancaria", Date = new DateTime(2023, 11, 15) },
                new Transaction { Id = 6, UserId = 3, AssetId = 2, Concept = "Compra de Ethereum", Amount = 2, PaymentMethod = "Tarjeta de Crédito", Date = new DateTime(2023, 8, 20) },
                new Transaction { Id = 7, UserId = 3, AssetId = 3, Concept = "Compra de Cardano", Amount = 1000, PaymentMethod = "Wallet", Date = new DateTime(2023, 9, 30) },
                new Transaction { Id = 8, UserId = 3, AssetId = 3, Concept = "Venta de Cardano", Amount = 500, PaymentMethod = "Transferencia Bancaria", Date = new DateTime(2023, 10, 21) },
                new Transaction { Id = 9, UserId = 4, AssetId = 1, Concept = "Compra de Bitcoin", Amount = 0.2, PaymentMethod = "Wallet", Date = new DateTime(2023, 6, 25) },
                new Transaction { Id = 10, UserId = 4, AssetId = 2, Concept = "Venta de Ethereum", Amount = 0.5, PaymentMethod = "Tarjeta de Crédito", Date = new DateTime(2023, 7, 14) },
                new Transaction { Id = 11, UserId = 4, AssetId = 1, Concept = "Compra de Bitcoin", Amount = 0.15, PaymentMethod = "Transferencia Bancaria", Date = new DateTime(2023, 10, 6) },
                new Transaction { Id = 12, UserId = 2, AssetId = 1, Concept = "Compra de acciones de Apple", Amount = 10, PaymentMethod = "Wallet", Date = new DateTime(2023, 9, 7) },
                new Transaction { Id = 13, UserId = 3, AssetId = 2, Concept = "Compra de acciones de Microsoft", Amount = 5, PaymentMethod = "Tarjeta de Crédito", Date = new DateTime(2023, 8, 16) },
                new Transaction { Id = 14, UserId = 4, AssetId = 3, Concept = "Venta de acciones de Tesla", Amount = 2, PaymentMethod = "Transferencia Bancaria", Date = new DateTime(2023, 9, 18) }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
    }
}
