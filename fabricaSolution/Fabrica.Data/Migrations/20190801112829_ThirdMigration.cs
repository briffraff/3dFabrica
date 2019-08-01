using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrica.Data.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarvelousProps",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Hashtags = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MarvelousOwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarvelousProps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarvelousProps_AspNetUsers_MarvelousOwnerId",
                        column: x => x.MarvelousOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarvelousProps_MarvelousOwnerId",
                table: "MarvelousProps",
                column: "MarvelousOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarvelousProps");
        }
    }
}
