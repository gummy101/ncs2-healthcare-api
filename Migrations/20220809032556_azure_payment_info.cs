using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApi.Migrations
{
    public partial class azure_payment_info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentInfoId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PaymentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreditCardNumber = table.Column<int>(type: "int", nullable: false),
                    CreditCardType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiryDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentInfoId",
                table: "Orders",
                column: "PaymentInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentInfos_PaymentInfoId",
                table: "Orders",
                column: "PaymentInfoId",
                principalTable: "PaymentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentInfos_PaymentInfoId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "PaymentInfos");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentInfoId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentInfoId",
                table: "Orders");
        }
    }
}
