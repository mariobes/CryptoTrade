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
                    MarketCap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Ranking = table.Column<int>(type: "int", nullable: false),
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
                    Ranking = table.Column<int>(type: "int", nullable: false),
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
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                columns: new[] { "Id", "AllTimeHigh", "AllTimeLow", "CirculatingSupply", "Contract", "CreationDate", "Description", "MarketCap", "Name", "Ranking", "Symbol", "TotalSupply", "Value", "Website" },
                values: new object[,]
                {
                    { 1, 69000.0, 67.0, 19000000.0, "0x0000000000000000000000000000000000000000", new DateTime(2009, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "La primera criptomoneda descentralizada", "900B", "Bitcoin", 1, "BTC", 21000000.0, 60000.0, "https://bitcoin.org" },
                    { 2, 4800.0, 0.41999999999999998, 118000000.0, "0x2170ed0880ac9a755fd29b2688956bd959f933f8", new DateTime(2015, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Una plataforma descentralizada de contratos inteligentes", "400B", "Ethereum", 2, "ETH", 118000000.0, 4000.0, "https://ethereum.org" },
                    { 3, 3.1000000000000001, 0.017000000000000001, 32000000000.0, "0x3ee2200efb3400fabb9aacf31297cbdd1d435d47", new DateTime(2017, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Plataforma blockchain de tercera generación", "70B", "Cardano", 5, "ADA", 45000000000.0, 2.1499999999999999, "https://cardano.org" }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Category", "CompanyValue", "CreationDate", "Description", "DividendYield", "EarningPerShare", "Name", "Ranking", "Value", "Website" },
                values: new object[,]
                {
                    { 1, "Technology", 2500000000000.0, new DateTime(1976, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empresa multinacional de tecnología", 0.60999999999999999, 5.6100000000000003, "Apple Inc.", 1, 150.25, "https://www.apple.com" },
                    { 2, "Technology", 2300000000000.0, new DateTime(1975, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empresa de software y hardware", 0.87, 9.7799999999999994, "Microsoft Corporation", 2, 300.55000000000001, "https://www.microsoft.com" },
                    { 3, "Automotive", 800000000000.0, new DateTime(2003, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Empresa de automóviles eléctricos y energías limpias", 0.0, 2.1600000000000001, "Tesla, Inc.", 3, 750.75, "https://www.tesla.com" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthdate", "Cash", "DNI", "Email", "IsBanned", "Name", "Nationality", "Password", "Phone", "Role", "Wallet" },
                values: new object[,]
                {
                    { 1, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "32452464D", "mario@gmail.com", false, "Mario", "España", "mario12345", "4574", "admin", 0.0 },
                    { 2, new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 146.0, "23523562D", "carlos@gmail.com", false, "Carlos", "Argentina", "carlos12345", "4567477", "user", 350.0 },
                    { 3, new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "23526445X", "fernando@gmail.com", false, "Fernando", "España", "fernando12345", "4745", "user", 0.0 },
                    { 4, new DateTime(2004, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "52353425D", "eduardo@gmail.com", false, "Eduardo", "España", "eduardo12345", "4574548", "user", 0.0 }
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
