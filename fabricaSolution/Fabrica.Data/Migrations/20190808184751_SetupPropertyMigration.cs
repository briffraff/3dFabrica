using Microsoft.EntityFrameworkCore.Migrations;

namespace Fabrica.Data.Migrations
{
    public partial class SetupPropertyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_MarvelousProps_MarvelousPropId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Props_PropId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MarvelousPropId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PropId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MarvelousPropId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PropId",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CreditAccountId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreditAccount",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CardNumber = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    Cash = table.Column<double>(nullable: false),
                    AccountOwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditAccount_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarvelousPropOrder",
                columns: table => new
                {
                    MarvelousPropId = table.Column<string>(nullable: false),
                    OrderId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarvelousPropOrder", x => new { x.MarvelousPropId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_MarvelousPropOrder_MarvelousProps_MarvelousPropId",
                        column: x => x.MarvelousPropId,
                        principalTable: "MarvelousProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarvelousPropOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropOrder",
                columns: table => new
                {
                    PropId = table.Column<string>(nullable: false),
                    OrderId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropOrder", x => new { x.PropId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_PropOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropOrder_Props_PropId",
                        column: x => x.PropId,
                        principalTable: "Props",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarvelousPropOrder_OrderId",
                table: "MarvelousPropOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PropOrder_OrderId",
                table: "PropOrder",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditAccount");

            migrationBuilder.DropTable(
                name: "MarvelousPropOrder");

            migrationBuilder.DropTable(
                name: "PropOrder");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreditAccountId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "MarvelousPropId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MarvelousPropId",
                table: "Orders",
                column: "MarvelousPropId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PropId",
                table: "Orders",
                column: "PropId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_MarvelousProps_MarvelousPropId",
                table: "Orders",
                column: "MarvelousPropId",
                principalTable: "MarvelousProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Props_PropId",
                table: "Orders",
                column: "PropId",
                principalTable: "Props",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
