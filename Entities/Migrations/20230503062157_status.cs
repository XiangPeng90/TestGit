using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShopCancel",
                table: "T_Order");

            migrationBuilder.DropColumn(
                name: "IsShopDelete",
                table: "T_Order");

            migrationBuilder.DropColumn(
                name: "IsUserCancel",
                table: "T_Order");

            migrationBuilder.DropColumn(
                name: "IsUserDelete",
                table: "T_Order");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "T_Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "T_Order");

            migrationBuilder.AddColumn<bool>(
                name: "IsShopCancel",
                table: "T_Order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShopDelete",
                table: "T_Order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUserCancel",
                table: "T_Order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUserDelete",
                table: "T_Order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
