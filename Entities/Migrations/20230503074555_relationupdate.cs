using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class relationupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Food_T_Order_Orderid",
                table: "T_Food");

            migrationBuilder.DropForeignKey(
                name: "FK_T_Food_t_trolley_TrolleyId",
                table: "T_Food");

            migrationBuilder.DropIndex(
                name: "IX_T_Food_Orderid",
                table: "T_Food");

            migrationBuilder.DropIndex(
                name: "IX_T_Food_TrolleyId",
                table: "T_Food");

            migrationBuilder.DropColumn(
                name: "Orderid",
                table: "T_Food");

            migrationBuilder.DropColumn(
                name: "TrolleyId",
                table: "T_Food");

            migrationBuilder.CreateTable(
                name: "FoodOrder",
                columns: table => new
                {
                    Foodsid = table.Column<long>(type: "bigint", nullable: false),
                    ordersid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOrder", x => new { x.Foodsid, x.ordersid });
                    table.ForeignKey(
                        name: "FK_FoodOrder_T_Food_Foodsid",
                        column: x => x.Foodsid,
                        principalTable: "T_Food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodOrder_T_Order_ordersid",
                        column: x => x.ordersid,
                        principalTable: "T_Order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FoodTrolley",
                columns: table => new
                {
                    foodsid = table.Column<long>(type: "bigint", nullable: false),
                    trolleysId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTrolley", x => new { x.foodsid, x.trolleysId });
                    table.ForeignKey(
                        name: "FK_FoodTrolley_T_Food_foodsid",
                        column: x => x.foodsid,
                        principalTable: "T_Food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodTrolley_t_trolley_trolleysId",
                        column: x => x.trolleysId,
                        principalTable: "t_trolley",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FoodOrder_ordersid",
                table: "FoodOrder",
                column: "ordersid");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTrolley_trolleysId",
                table: "FoodTrolley",
                column: "trolleysId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodOrder");

            migrationBuilder.DropTable(
                name: "FoodTrolley");

            migrationBuilder.AddColumn<long>(
                name: "Orderid",
                table: "T_Food",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TrolleyId",
                table: "T_Food",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Food_Orderid",
                table: "T_Food",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_T_Food_TrolleyId",
                table: "T_Food",
                column: "TrolleyId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Food_T_Order_Orderid",
                table: "T_Food",
                column: "Orderid",
                principalTable: "T_Order",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Food_t_trolley_TrolleyId",
                table: "T_Food",
                column: "TrolleyId",
                principalTable: "t_trolley",
                principalColumn: "Id");
        }
    }
}
