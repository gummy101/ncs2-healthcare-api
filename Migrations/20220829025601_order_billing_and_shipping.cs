using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApi.Migrations
{
    public partial class order_billing_and_shipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillingAddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShippingAddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingAddressId",
                table: "Orders",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserAddress_BillingAddressId",
                table: "Orders",
                column: "BillingAddressId",
                principalTable: "UserAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserAddress_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId",
                principalTable: "UserAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserAddress_BillingAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserAddress_ShippingAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BillingAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddressId",
                table: "Orders");
        }
    }
}
