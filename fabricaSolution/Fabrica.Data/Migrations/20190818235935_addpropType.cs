using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrica.Data.Migrations
{
    public partial class addpropType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Props");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MarvelousProps");

            migrationBuilder.AddColumn<int>(
                name: "propType",
                table: "Props",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "propType",
                table: "MarvelousProps",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "propType",
                table: "Props");

            migrationBuilder.DropColumn(
                name: "propType",
                table: "MarvelousProps");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Props",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "MarvelousProps",
                nullable: false,
                defaultValue: 0);
        }
    }
}
