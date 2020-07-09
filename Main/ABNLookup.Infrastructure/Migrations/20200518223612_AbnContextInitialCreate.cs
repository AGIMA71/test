using Microsoft.EntityFrameworkCore.Migrations;

namespace ABNLookup.Infrastructure.Migrations
{
    public partial class AbnContextInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abn",
                columns: table => new
                {
                    ClientInternalId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ABNidentifierValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abn", x => x.ClientInternalId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abn_ABNidentifierValue",
                table: "Abn",
                column: "ABNidentifierValue",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abn");
        }
    }
}
