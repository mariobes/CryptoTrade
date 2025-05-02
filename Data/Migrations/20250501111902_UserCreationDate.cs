using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTrade.Data.Migrations
{
    public partial class UserCreationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 5, 1, 13, 19, 2, 289, DateTimeKind.Utc).AddTicks(7624), new DateTime(2025, 5, 1, 13, 19, 2, 289, DateTimeKind.Utc).AddTicks(7627) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 5, 1, 13, 19, 2, 289, DateTimeKind.Utc).AddTicks(7631), new DateTime(2025, 5, 1, 13, 19, 2, 289, DateTimeKind.Utc).AddTicks(7632) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 5, 1, 13, 19, 2, 289, DateTimeKind.Utc).AddTicks(7634), new DateTime(2025, 5, 1, 13, 19, 2, 289, DateTimeKind.Utc).AddTicks(7634) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2025, 5, 1, 13, 19, 2, 289, DateTimeKind.Utc).AddTicks(7656), new DateTime(2025, 5, 1, 13, 19, 2, 289, DateTimeKind.Utc).AddTicks(7657) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastUpdated",
                value: new DateTime(2025, 4, 30, 17, 7, 17, 723, DateTimeKind.Utc).AddTicks(8559));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "LastUpdated",
                value: new DateTime(2025, 4, 30, 17, 7, 17, 723, DateTimeKind.Utc).AddTicks(8564));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "LastUpdated",
                value: new DateTime(2025, 4, 30, 17, 7, 17, 723, DateTimeKind.Utc).AddTicks(8568));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "LastUpdated",
                value: new DateTime(2025, 4, 30, 17, 7, 17, 723, DateTimeKind.Utc).AddTicks(8569));
        }
    }
}
