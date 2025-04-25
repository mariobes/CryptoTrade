using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTrade.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoIndices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: true),
                    ChangePercentage = table.Column<double>(type: "float", nullable: true),
                    Sentiment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoIndices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cryptos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MarketCap = table.Column<double>(type: "float", nullable: false),
                    MarketCapRank = table.Column<double>(type: "float", nullable: true),
                    FullyDilutedValuation = table.Column<double>(type: "float", nullable: true),
                    TotalVolume = table.Column<double>(type: "float", nullable: true),
                    High24h = table.Column<double>(type: "float", nullable: true),
                    Low24h = table.Column<double>(type: "float", nullable: true),
                    PriceChange24h = table.Column<double>(type: "float", nullable: true),
                    PriceChangePercentage24h = table.Column<double>(type: "float", nullable: true),
                    PriceChangePercentage1h = table.Column<double>(type: "float", nullable: true),
                    PriceChangePercentage7d = table.Column<double>(type: "float", nullable: true),
                    MarketCapChange24h = table.Column<double>(type: "float", nullable: true),
                    MarketCapChangePercentage24h = table.Column<double>(type: "float", nullable: true),
                    CirculatingSupply = table.Column<double>(type: "float", nullable: true),
                    TotalSupply = table.Column<double>(type: "float", nullable: true),
                    MaxSupply = table.Column<double>(type: "float", nullable: true),
                    AllTimeHigh = table.Column<double>(type: "float", nullable: true),
                    AllTimeHighChangePercentage = table.Column<double>(type: "float", nullable: true),
                    AllTimeHighDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AllTimeLow = table.Column<double>(type: "float", nullable: true),
                    AllTimeLowChangePercentage = table.Column<double>(type: "float", nullable: true),
                    AllTimeLowDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SparklineIn7d = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoTrendings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    ChangePercentage = table.Column<double>(type: "float", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoTrendings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockGainers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    ChangePercentage = table.Column<double>(type: "float", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockGainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockLosers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    ChangePercentage = table.Column<double>(type: "float", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockLosers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockMostActives",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    ChangePercentage = table.Column<double>(type: "float", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMostActives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    MarketCap = table.Column<double>(type: "float", nullable: false),
                    MarketCapRank = table.Column<double>(type: "float", nullable: true),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAnnualDividend = table.Column<double>(type: "float", nullable: true),
                    Volume = table.Column<double>(type: "float", nullable: true),
                    Exchange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchangeShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Changes = table.Column<double>(type: "float", nullable: true),
                    ChangesPercentage = table.Column<double>(type: "float", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Isin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ceo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTrendings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    ChangePercentage = table.Column<double>(type: "float", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTrendings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cash = table.Column<double>(type: "float", nullable: false),
                    Wallet = table.Column<double>(type: "float", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBanned = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Concept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: true),
                    AssetAmount = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: true),
                    TypeOfAsset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CryptoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StockId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Cryptos_CryptoId",
                        column: x => x.CryptoId,
                        principalTable: "Cryptos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthdate", "Cash", "Currency", "DNI", "Email", "IsBanned", "Language", "Name", "Nationality", "Password", "Phone", "Role", "Theme", "Wallet" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10000.0, "USD", "Admin", "admin@cryptotrade.com", false, "ES", "Admin", "Admin", "Admin12345%", "000", "admin", "light", 0.0 },
                    { 2, new DateTime(2001, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 400.0, "USD", "25463652D", "mario@gmail.com", false, "ES", "Mario", "España", "Mario12345%", "567935418", "user", "light", 750.0 },
                    { 3, new DateTime(2003, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 300.0, "USD", "26587463X", "fernando@gmail.com", false, "ES", "Fernando", "España", "Fernando12345%", "541298637", "user", "light", 1650.0 },
                    { 4, new DateTime(1998, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 200.0, "USD", "52684659D", "eduardo@gmail.com", false, "ES", "Eduardo", "España", "Eduardo12345%", "658248974", "user", "light", 1020.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CryptoId",
                table: "Transactions",
                column: "CryptoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_StockId",
                table: "Transactions",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoIndices");

            migrationBuilder.DropTable(
                name: "CryptoTrendings");

            migrationBuilder.DropTable(
                name: "StockGainers");

            migrationBuilder.DropTable(
                name: "StockLosers");

            migrationBuilder.DropTable(
                name: "StockMostActives");

            migrationBuilder.DropTable(
                name: "StockTrendings");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Cryptos");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
