using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrica.Data.Migrations
{
    public partial class addPropType : Migration
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
                name: "PropType",
                table: "Props",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropType",
                table: "MarvelousProps",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropType",
                table: "Props");

            migrationBuilder.DropColumn(
                name: "PropType",
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
