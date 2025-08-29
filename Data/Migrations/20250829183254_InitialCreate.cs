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
                    Profit = table.Column<double>(type: "float", nullable: false),
                    LastBalance = table.Column<double>(type: "float", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Watchlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeAsset = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlists", x => x.Id);
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
                columns: new[] { "Id", "Birthdate", "Cash", "CreationDate", "DNI", "Email", "LastBalance", "LastUpdated", "Name", "Nationality", "Password", "Phone", "Profit", "Role", "Wallet" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "admin@cryptotrade.com", 0.0, new DateTime(2025, 8, 29, 20, 32, 54, 74, DateTimeKind.Utc).AddTicks(5915), "Admin", "Admin", "YHrp/ExR53lRO6ouA2tT0y9QCb94jfjNBsxcGq5x798=", "000", 0.0, "admin", 0.0 },
                    { 2, new DateTime(2001, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 384.35000000000002, new DateTime(2022, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "25463652D", "mario@gmail.com", 0.0, new DateTime(2025, 8, 29, 20, 32, 54, 74, DateTimeKind.Utc).AddTicks(5921), "Mario", "España", "JApd9lfG2wshq3agTXjgwVT/f4jQecLCYTBnBT30AqE=", "567935418", 0.0, "user", 4615.6499999999996 },
                    { 3, new DateTime(2003, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, new DateTime(2023, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "26587463X", "fernando@gmail.com", 0.0, new DateTime(2025, 8, 29, 20, 32, 54, 74, DateTimeKind.Utc).AddTicks(5923), "Fernando", "España", "xf0cyil3yRNj5rC2KE+3O+wmt/rGtUapwYkq5YfkqG4=", "541298637", 0.0, "user", 0.0 },
                    { 4, new DateTime(1998, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "52684659D", "eduardo@gmail.com", 0.0, new DateTime(2025, 8, 29, 20, 32, 54, 74, DateTimeKind.Utc).AddTicks(5924), "Eduardo", "España", "6GGegrjO3tQMHPZBrkdANTfPC92ka20ChXH9VdvhLak=", "658248974", 0.0, "user", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Watchlists",
                columns: new[] { "Id", "AssetId", "TypeAsset", "UserId" },
                values: new object[,]
                {
                    { 1, "bitcoin", "Crypto", 2 },
                    { 2, "ethereum", "Crypto", 2 },
                    { 3, "ripple", "Crypto", 2 },
                    { 4, "cardano", "Crypto", 2 },
                    { 5, "aapl", "Stock", 2 },
                    { 6, "amzn", "Stock", 2 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "AssetAmount", "AssetId", "Concept", "CryptoId", "Date", "PaymentMethod", "PurchasePrice", "StockId", "TypeOfAsset", "UserId" },
                values: new object[,]
                {
                    { 1, 5000.0, null, null, "+ Deposit", null, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, null, 2 },
                    { 2, 109.44, 0.0048780000000000004, "bitcoin", "+ Bitcoin", null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 22436.130000000001, null, "Crypto", 2 },
                    { 3, 218.88999999999999, 0.0076920000000000001, "bitcoin", "+ Bitcoin", null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 28455.57, null, "Crypto", 2 },
                    { 4, 54.719999999999999, 0.001196, "bitcoin", "+ Bitcoin", null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 45749.959999999999, null, "Crypto", 2 },
                    { 5, 54.719999999999999, 0.001276, "bitcoin", "+ Bitcoin", null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 42904.269999999997, null, "Crypto", 2 },
                    { 6, 109.45999999999999, 0.0024940000000000001, "bitcoin", "+ Bitcoin", null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 43891.480000000003, null, "Crypto", 2 },
                    { 7, 654.37, 7331.1300000000001, "hedera-hashgraph", "+ Hedera", null, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.089260000000000006, null, "Crypto", 2 },
                    { 8, 529.36000000000001, 1172.51, "cardano", "+ Cardano", null, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.45140000000000002, null, "Crypto", 2 },
                    { 9, 322.91000000000003, 534.22000000000003, "ripple", "+ XRP", null, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.60450000000000004, null, "Crypto", 2 },
                    { 10, 203.19999999999999, 263.00999999999999, "ondo-finance", "+ Ondo", null, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.77259999999999995, null, "Crypto", 2 },
                    { 11, 507.43000000000001, 0.2175, "ethereum", "+ Ethereum", null, new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2332.9200000000001, null, "Crypto", 2 },
                    { 12, 229.09, 35.700000000000003, "polkadot", "+ Polkadot", null, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6.4169999999999998, null, "Crypto", 2 },
                    { 13, 195.47, 30.460000000000001, "polkadot", "+ Polkadot", null, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6.4169999999999998, null, "Crypto", 2 },
                    { 14, 157.43000000000001, 23400000.0, "pepe", "+ Pepe", null, new DateTime(2024, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6.72E-06, null, "Crypto", 2 },
                    { 15, 200.00999999999999, 25.899999999999999, "near", "+ NEAR Protocol", null, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 7.7210999999999999, null, "Crypto", 2 },
                    { 16, 309.94, 62.939999999999998, "near", "+ NEAR Protocol", null, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4.9240000000000004, null, "Crypto", 2 },
                    { 17, 309.31999999999999, 44.939999999999998, "render-token", "+ Render", null, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 6.883, null, "Crypto", 2 },
                    { 18, 99.890000000000001, 23.989999999999998, "render-token", "+ Render", null, new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4.1639999999999997, null, "Crypto", 2 },
                    { 19, 100.0, 0.3125, "msft", "+ Microsoft Corporation", null, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 320.0, null, "Stock", 2 },
                    { 20, 250.0, 2.0830000000000002, "nvda", "+ NVIDIA Corporation", null, new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 120.0, null, "Stock", 2 }
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
                name: "Watchlists");

            migrationBuilder.DropTable(
                name: "Cryptos");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
