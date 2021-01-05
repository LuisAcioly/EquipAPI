using Microsoft.EntityFrameworkCore.Migrations;

namespace equip.api.Migrations
{
    public partial class InitiaCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "TB_EQUIPMENT",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    UserCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EQUIPMENT", x => x.Code);
                    table.ForeignKey(
                        name: "FK_TB_EQUIPMENT_TB_USER_UserCode",
                        column: x => x.UserCode,
                        principalTable: "TB_USER",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_EQUIPMENT_UserCode",
                table: "TB_EQUIPMENT",
                column: "UserCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_EQUIPMENT");

            migrationBuilder.DropTable(
                name: "TB_USER");
        }
    }
}
