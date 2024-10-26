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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging(); // Opcional
        }
    }
}
