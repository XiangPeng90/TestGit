using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TrolleyId",
                table: "T_Food",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "t_trolley",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_trolley", x => x.Id);
                    table.ForeignKey(
                        name: "FK_t_trolley_T_User_userId",
                        column: x => x.userId,
                        principalTable: "T_User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_T_Food_TrolleyId",
                table: "T_Food",
                column: "TrolleyId");

            migrationBuilder.CreateIndex(
                name: "IX_t_trolley_userId",
                table: "t_trolley",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_T_Food_t_trolley_TrolleyId",
                table: "T_Food",
                column: "TrolleyId",
                principalTable: "t_trolley",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Food_t_trolley_TrolleyId",
                table: "T_Food");

            migrationBuilder.DropTable(
                name: "t_trolley");

            migrationBuilder.DropIndex(
                name: "IX_T_Food_TrolleyId",
                table: "T_Food");

            migrationBuilder.DropColumn(
                name: "TrolleyId",
                table: "T_Food");
        }
    }
}
