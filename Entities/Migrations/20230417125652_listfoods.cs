using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class listfoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Orderid",
                table: "T_Food",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Food_Orderid",
                table: "T_Food",
                column: "Orderid");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Food_T_Order_Orderid",
                table: "T_Food",
                column: "Orderid",
                principalTable: "T_Order",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Food_T_Order_Orderid",
                table: "T_Food");

            migrationBuilder.DropIndex(
                name: "IX_T_Food_Orderid",
                table: "T_Food");

            migrationBuilder.DropColumn(
                name: "Orderid",
                table: "T_Food");
        }
    }
}
