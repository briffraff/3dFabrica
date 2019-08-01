using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrica.Data.Migrations
{
    public partial class FourMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MarvelousProps",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PropId = table.Column<string>(nullable: true),
                    MarvelousPropId = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    OrderedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_MarvelousProps_MarvelousPropId",
                        column: x => x.MarvelousPropId,
                        principalTable: "MarvelousProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Props_PropId",
                        column: x => x.PropId,
                        principalTable: "Props",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MarvelousPropId",
                table: "Order",
                column: "MarvelousPropId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PropId",
                table: "Order",
                column: "PropId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MarvelousProps");
        }
    }
}
