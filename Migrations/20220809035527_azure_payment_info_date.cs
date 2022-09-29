using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApi.Migrations
{
    public partial class azure_payment_info_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreditCardNumber",
                table: "PaymentInfos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PaymentInfos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "PaymentInfos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PaymentInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInfos_UserId",
                table: "PaymentInfos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInfos_Users_UserId",
                table: "PaymentInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInfos_Users_UserId",
                table: "PaymentInfos");

            migrationBuilder.DropIndex(
                name: "IX_PaymentInfos_UserId",
                table: "PaymentInfos");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "PaymentInfos");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "PaymentInfos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentInfos");

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardNumber",
                table: "PaymentInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
