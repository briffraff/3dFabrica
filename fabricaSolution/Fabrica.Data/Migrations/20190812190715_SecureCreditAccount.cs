using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrica.Data.Migrations
{
    public partial class SecureCreditAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthNumber",
                table: "CreditAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthNumber",
                table: "CreditAccounts");
        }
    }
}
