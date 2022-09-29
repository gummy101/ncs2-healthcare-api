using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApi.Migrations
{
    public partial class azure_item_qty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartItem");
        }
    }
}
