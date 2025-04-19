using Microsoft.EntityFrameworkCore;
using CryptoTrade.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

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
                new User { Id = 1, Name = "Admin", Birthdate = new DateTime(0001, 01, 01), Email = "admin@cryptotrade.com", Password = "Admin12345%", Phone = "000", DNI = "Admin", Nationality = "Admin", Cash = 10000, Wallet = 0, Role = Roles.Admin},
                new User { Id = 2, Name = "Mario", Birthdate = new DateTime(2001, 09, 17), Email = "mario@gmail.com", Password = "Mario12345%", Phone = "567935418", DNI = "25463652D", Nationality = "España", Cash = 400, Wallet = 750 },
                new User { Id = 3, Name = "Fernando", Birthdate = new DateTime(2003, 01, 28), Email = "fernando@gmail.com", Password = "Fernando12345%", Phone = "541298637", DNI = "26587463X", Nationality = "España", Cash = 300, Wallet = 1650 },
                new User { Id = 4, Name = "Eduardo", Birthdate = new DateTime(1998, 11, 04), Email = "eduardo@gmail.com", Password = "Eduardo12345%", Phone = "658248974", DNI = "52684659D", Nationality = "España", Cash = 200, Wallet = 1020 }
            );

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

            modelBuilder.Entity<Crypto>(entity =>
            {
                entity.Property(e => e.SparklineIn7d)
                    .HasConversion(
                        v => v != null && v.Price != null && v.Price.Any()
                            ? string.Join(",", v.Price.Select(price => price.ToString("0.0000", CultureInfo.InvariantCulture)))  // Ahora con 4 decimales
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
}
