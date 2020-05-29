using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionOnline.Migrations
{
    public partial class abc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BidIncrementDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,1)", nullable: false),
                    BidIncrement = table.Column<decimal>(type: "decimal(18,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidIncrementDefinition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BidStatus = table.Column<int>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    Document = table.Column<string>(nullable: true),
                    BidStartDate = table.Column<DateTime>(nullable: false),
                    BidEndDate = table.Column<DateTime>(nullable: false),
                    BidIncrementId = table.Column<int>(nullable: false),
                    MinimumBid = table.Column<decimal>(type: "decimal(18,1)", nullable: false),
                    BidIncrementDefinitionId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_BidIncrementDefinition_BidIncrementDefinitionId",
                        column: x => x.BidIncrementDefinitionId,
                        principalTable: "BidIncrementDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Furniture" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electric" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_BidIncrementDefinitionId",
                table: "Items",
                column: "BidIncrementDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "BidIncrementDefinition");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
