using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApi.Migrations
{
    public partial class new_payment_info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVV",
                table: "PaymentInfos");

            migrationBuilder.DropColumn(
                name: "CreditCardNumber",
                table: "PaymentInfos");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "PaymentInfos");

            migrationBuilder.RenameColumn(
                name: "CreditCardType",
                table: "PaymentInfos",
                newName: "AccountNumber");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "PaymentInfos",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "PaymentInfos");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "PaymentInfos",
                newName: "CreditCardType");

            migrationBuilder.AddColumn<int>(
                name: "CVV",
                table: "PaymentInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreditCardNumber",
                table: "PaymentInfos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "PaymentInfos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
