using Microsoft.EntityFrameworkCore.Migrations;

namespace TCGShop.Migrations
{
    public partial class ordercahnged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Order",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Order",
                newName: "ID_User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Order",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "ID_User",
                table: "Order",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Order",
                type: "text",
                nullable: true);
        }
    }
}
