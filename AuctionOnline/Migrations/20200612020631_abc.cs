using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionOnline.Migrations
{
    public partial class abc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BidIncrementDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceRange = table.Column<decimal>(type: "decimal(18,1)", nullable: false),
                    BidIncrement = table.Column<decimal>(type: "decimal(18,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidIncrementDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,1)", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    Document = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    BidStatus = table.Column<bool>(nullable: false),
                    BidStartDate = table.Column<DateTime>(nullable: true),
                    BidEndDate = table.Column<DateTime>(nullable: true),
                    MinimumBid = table.Column<decimal>(type: "decimal(18,1)", nullable: true),
                    CurrentBid = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    BidIncrementDefinitionId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bids_BidIncrementDefinitions_BidIncrementDefinitionId",
                        column: x => x.BidIncrementDefinitionId,
                        principalTable: "BidIncrementDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bids_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItems", x => new { x.ItemId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CategoryItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "Fullname", "IsBlocked", "Password", "RoleId", "Status", "Username" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "Admin", false, "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", 0, true, "admin123" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1@gmail.com", "User 1", false, "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", 1, true, "user1" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2@gmail.com", "User 2", false, "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", 1, true, "user2" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Furniture", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electric", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentId" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smarts", 2 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentId" },
                values: new object[] { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laptops & Macs", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AccountId",
                table: "Bids",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_BidIncrementDefinitionId",
                table: "Bids",
                column: "BidIncrementDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ItemId",
                table: "Bids",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_CategoryId",
                table: "CategoryItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_AccountId",
                table: "Items",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "CategoryItems");

            migrationBuilder.DropTable(
                name: "BidIncrementDefinitions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
