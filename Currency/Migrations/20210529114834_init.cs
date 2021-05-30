using Microsoft.EntityFrameworkCore.Migrations;

namespace Currency.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Valcures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valcures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Valutes",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CharCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nominal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValCursId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valutes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Valutes_Valcures_ValCursId",
                        column: x => x.ValCursId,
                        principalTable: "Valcures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Valutes_ValCursId",
                table: "Valutes",
                column: "ValCursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Valutes");

            migrationBuilder.DropTable(
                name: "Valcures");
        }
    }
}
