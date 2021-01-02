using Microsoft.EntityFrameworkCore.Migrations;

namespace TCGShop.Migrations
{
    public partial class ordercartid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_Cart",
                table: "Order",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_Cart",
                table: "Order");
        }
    }
}
