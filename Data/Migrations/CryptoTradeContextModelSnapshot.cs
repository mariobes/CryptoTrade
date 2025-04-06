﻿// <auto-generated />
using System;
using CryptoTrade.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CryptoTrade.Data.Migrations
{
    [DbContext(typeof(CryptoTradeContext))]
    partial class CryptoTradeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Crypto", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("AllTimeHigh")
                        .HasColumnType("float");

                    b.Property<double?>("AllTimeHighChangePercentage")
                        .HasColumnType("float");

                    b.Property<DateTime?>("AllTimeHighDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("AllTimeLow")
                        .HasColumnType("float");

                    b.Property<double?>("AllTimeLowChangePercentage")
                        .HasColumnType("float");

                    b.Property<DateTime?>("AllTimeLowDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("CirculatingSupply")
                        .HasColumnType("float");

                    b.Property<double?>("FullyDilutedValuation")
                        .HasColumnType("float");

                    b.Property<double?>("High24h")
                        .HasColumnType("float");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Low24h")
                        .HasColumnType("float");

                    b.Property<double?>("MarketCap")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("MarketCapChange24h")
                        .HasColumnType("float");

                    b.Property<double?>("MarketCapChangePercentage24h")
                        .HasColumnType("float");

                    b.Property<double?>("MarketCapRank")
                        .HasColumnType("float");

                    b.Property<double?>("MaxSupply")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("PriceChange24h")
                        .HasColumnType("float");

                    b.Property<double?>("PriceChangePercentage1h")
                        .HasColumnType("float");

                    b.Property<double?>("PriceChangePercentage24h")
                        .HasColumnType("float");

                    b.Property<double?>("PriceChangePercentage7d")
                        .HasColumnType("float");

                    b.Property<string>("SparklineIn7d")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("TotalSupply")
                        .HasColumnType("float");

                    b.Property<double?>("TotalVolume")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Cryptos");
                });

            modelBuilder.Entity("CryptoTrade.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<double?>("AssetAmount")
                        .HasColumnType("float");

                    b.Property<string>("AssetId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Concept")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CryptoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<double?>("PurchasePrice")
                        .HasColumnType("float");

                    b.Property<string>("StockId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TypeOfAsset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CryptoId");

                    b.HasIndex("StockId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CryptoTrade.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Cash")
                        .HasColumnType("float");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Wallet")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthdate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Cash = 10000.0,
                            Currency = "USD",
                            DNI = "ADMIN",
                            Email = "admin@cryptotrade.com",
                            IsBanned = false,
                            Language = "ES",
                            Name = "Admin",
                            Nationality = "ADMIN",
                            Password = "admin12345",
                            Phone = "0000",
                            Role = "admin",
                            Theme = "light",
                            Wallet = 0.0
                        },
                        new
                        {
                            Id = 2,
                            Birthdate = new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Cash = 400.0,
                            Currency = "USD",
                            DNI = "23523562D",
                            Email = "mario@gmail.com",
                            IsBanned = false,
                            Language = "ES",
                            Name = "Mario",
                            Nationality = "Argentina",
                            Password = "mario12345",
                            Phone = "4567477",
                            Role = "user",
                            Theme = "light",
                            Wallet = 750.0
                        },
                        new
                        {
                            Id = 3,
                            Birthdate = new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Cash = 300.0,
                            Currency = "USD",
                            DNI = "23526445X",
                            Email = "fernando@gmail.com",
                            IsBanned = false,
                            Language = "ES",
                            Name = "Fernando",
                            Nationality = "España",
                            Password = "fernando12345",
                            Phone = "4745",
                            Role = "user",
                            Theme = "light",
                            Wallet = 1650.0
                        },
                        new
                        {
                            Id = 4,
                            Birthdate = new DateTime(2004, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Cash = 200.0,
                            Currency = "USD",
                            DNI = "52353425D",
                            Email = "eduardo@gmail.com",
                            IsBanned = false,
                            Language = "ES",
                            Name = "Eduardo",
                            Nationality = "España",
                            Password = "eduardo12345",
                            Phone = "4574548",
                            Role = "user",
                            Theme = "light",
                            Wallet = 1020.0
                        });
                });

            modelBuilder.Entity("Stock", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Ceo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Changes")
                        .HasColumnType("float");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Exchange")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExchangeShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Industry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("LastAnnualDividend")
                        .HasColumnType("float");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<double?>("MarketCap")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<string>("Sector")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Volume")
                        .HasColumnType("float");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("CryptoTrade.Models.Transaction", b =>
                {
                    b.HasOne("Crypto", "Crypto")
                        .WithMany()
                        .HasForeignKey("CryptoId");

                    b.HasOne("Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId");

                    b.HasOne("CryptoTrade.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crypto");

                    b.Navigation("Stock");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CryptoTrade.Models.User", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
