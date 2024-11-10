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
                name: "Cryptos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketCap = table.Column<double>(type: "float", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSupply = table.Column<double>(type: "float", nullable: false),
                    CirculatingSupply = table.Column<double>(type: "float", nullable: false),
                    Contract = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllTimeHigh = table.Column<double>(type: "float", nullable: false),
                    AllTimeLow = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyValue = table.Column<double>(type: "float", nullable: false),
                    EarningPerShare = table.Column<double>(type: "float", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DividendYield = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
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
                    AssetId = table.Column<int>(type: "int", nullable: true),
                    Concept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: true),
                    AssetAmount = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: true),
                    TypeOfAsset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CryptoId = table.Column<int>(type: "int", nullable: true),
                    StockId = table.Column<int>(type: "int", nullable: true)
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
                table: "Cryptos",
                columns: new[] { "Id", "AllTimeHigh", "AllTimeLow", "CirculatingSupply", "Contract", "CreationDate", "Description", "MarketCap", "Name", "Symbol", "TotalSupply", "Value", "Website" },
                values: new object[,]
                {
                    { 1, 69000.0, 67.0, 19000000.0, "0x0000000000000000000000000000000000000000", new DateTime(2009, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "La primera criptomoneda descentralizada", 900000000000.0, "Bitcoin", "BTC", 21000000.0, 60000.0, "https://bitcoin.org" },
                    { 2, 4800.0, 0.41999999999999998, 118000000.0, "0x2170ed0880ac9a755fd29b2688956bd959f933f8", new DateTime(2015, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Una plataforma descentralizada de contratos inteligentes", 400000000000.0, "Ethereum", "ETH", 118000000.0, 4000.0, "https://ethereum.org" },
                    { 3, 3.1000000000000001, 0.017000000000000001, 32000000000.0, "0x3ee2200efb3400fabb9aacf31297cbdd1d435d47", new DateTime(2017, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Plataforma blockchain de tercera generación", 70000000000.0, "Cardano", "ADA", 45000000000.0, 2.1499999999999999, "https://cardano.org" }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Category", "CompanyValue", "CreationDate", "Description", "DividendYield", "EarningPerShare", "Name", "Value", "Website" },
                values: new object[,]
                {
                    { 1, "Technology", 2500000000000.0, new DateTime(1976, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empresa multinacional de tecnología", 0.60999999999999999, 5.6100000000000003, "Apple Inc.", 150.25, "https://www.apple.com" },
                    { 2, "Technology", 2300000000000.0, new DateTime(1975, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empresa de software y hardware", 0.87, 9.7799999999999994, "Microsoft Corporation", 300.55000000000001, "https://www.microsoft.com" },
                    { 3, "Automotive", 800000000000.0, new DateTime(2003, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empresa de automóviles eléctricos y energías limpias", 0.0, 2.1600000000000001, "Tesla Inc.", 750.75, "https://www.tesla.com" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthdate", "Cash", "DNI", "Email", "IsBanned", "Name", "Nationality", "Password", "Phone", "Role", "Wallet" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10000.0, "ADMIN", "admin@cryptotrade.com", false, "Admin", "ADMIN", "admin12345", "0000", "admin", 0.0 },
                    { 2, new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 400.0, "23523562D", "mario@gmail.com", false, "Mario", "Argentina", "mario12345", "4567477", "user", 750.0 },
                    { 3, new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 300.0, "23526445X", "fernando@gmail.com", false, "Fernando", "España", "fernando12345", "4745", "user", 1650.0 },
                    { 4, new DateTime(2004, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 200.0, "52353425D", "eduardo@gmail.com", false, "Eduardo", "España", "eduardo12345", "4574548", "user", 1020.0 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "AssetAmount", "AssetId", "Concept", "CryptoId", "Date", "PaymentMethod", "PurchasePrice", "StockId", "TypeOfAsset", "UserId" },
                values: new object[,]
                {
                    { 1, 1200.0, null, null, "Ingresar dinero", null, new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, null, null, null, 2 },
                    { 2, 2000.0, null, null, "Ingresar dinero", null, new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, null, 3 },
                    { 3, 1000.0, null, null, "Ingresar dinero", null, new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, null, 4 },
                    { 4, 100.0, 0.00167, 1, "Comprar Bitcoin", null, new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 60000.0, null, "Crypto", 2 },
                    { 5, 200.0, 0.050000000000000003, 2, "Comprar Ethereum", null, new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4000.0, null, "Crypto", 2 },
                    { 6, 500.0, 232.56, 3, "Comprar Cardano", null, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2.1499999999999999, null, "Crypto", 2 },
                    { 7, 50.0, 0.00083000000000000001, 1, "Vender Bitcoin", null, new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 60000.0, null, "Crypto", 2 },
                    { 8, 100.0, 0.00167, 1, "Comprar Bitcoin", null, new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 60000.0, null, "Crypto", 2 },
                    { 9, 50.0, 0.012500000000000001, 2, "Comprar Ethereum", null, new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4000.0, null, "Crypto", 3 },
                    { 10, 1000.0, 465.11000000000001, 3, "Comprar Cardano", null, new DateTime(2023, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2.1499999999999999, null, "Crypto", 3 },
                    { 11, 100.0, 46.509999999999998, 3, "Vender Cardano", null, new DateTime(2023, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2.1499999999999999, null, "Crypto", 3 },
                    { 12, 200.0, 0.0050000000000000001, 2, "Comprar Ethereum", null, new DateTime(2023, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 60000.0, null, "Crypto", 4 },
                    { 13, 20.0, 0.0050000000000000001, 2, "Vender Ethereum", null, new DateTime(2023, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4000.0, null, "Crypto", 4 },
                    { 14, 500.0, 0.0083300000000000006, 1, "Comprar Bitcoin", null, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 60000.0, null, "Crypto", 4 },
                    { 15, 100.0, 0.66500000000000004, 1, "Comprar Apple Inc.", null, new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 150.25, null, "Stock", 2 },
                    { 16, 500.0, 1.6639999999999999, 2, "Comprar Microsoft Corporation", null, new DateTime(2023, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 300.55000000000001, null, "Stock", 3 },
                    { 17, 200.0, 0.13300000000000001, 3, "Vender Tesla Inc.", null, new DateTime(2023, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 750.75, null, "Stock", 4 }
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
