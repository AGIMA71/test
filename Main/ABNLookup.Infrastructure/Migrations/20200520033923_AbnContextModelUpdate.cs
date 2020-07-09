using Microsoft.EntityFrameworkCore.Migrations;

namespace ABNLookup.Infrastructure.Migrations
{
    public partial class AbnContextModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ACNidentifierValue",
                table: "Abn",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainNameorganisationName",
                table: "Abn",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MessageCode",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageCode", x => x.Code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageCode");

            migrationBuilder.DropColumn(
                name: "ACNidentifierValue",
                table: "Abn");

            migrationBuilder.DropColumn(
                name: "MainNameorganisationName",
                table: "Abn");
        }
    }
}
