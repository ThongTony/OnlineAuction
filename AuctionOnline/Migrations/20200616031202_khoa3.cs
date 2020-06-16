using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionOnline.Migrations
{
    public partial class khoa3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_BidIncrementDefinitions_BidIncrementDefinitionId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "AccountItems");

            migrationBuilder.DropTable(
                name: "BidIncrementDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_Items_BidIncrementDefinitionId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BidIncrementDefinitionId",
                table: "Items");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumBid",
                table: "Items",
                type: "decimal(18,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,1)");

            migrationBuilder.AlterColumn<bool>(
                name: "BidStatus",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BidStartDate",
                table: "Items",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BidEndDate",
                table: "Items",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "BidIncrement",
                table: "Items",
                type: "decimal(18,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    CurrentBid = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
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
                        name: "FK_Bids_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationProducts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "Fullname", "IsBlocked", "Password", "RoleId", "Status", "Username" },
                values: new object[] { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1@gmail.com", "User 1", false, "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", 1, true, "user1" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "Fullname", "IsBlocked", "Password", "RoleId", "Status", "Username" },
                values: new object[] { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2@gmail.com", "User 2", false, "$2y$12$cxOGZj/S7yYv1waxPxyZweMygntL37mkvvUqtLFzeX1QW/mOt2bpG", 1, true, "user2" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentId" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smarts", 2 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "ParentId" },
                values: new object[] { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Laptops & Macs", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Items_AccountId",
                table: "Items",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_AccountId",
                table: "Bids",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ItemId",
                table: "Bids",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Accounts_AccountId",
                table: "Items",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Accounts_AccountId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "NotificationProducts");

            migrationBuilder.DropIndex(
                name: "IX_Items_AccountId",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BidIncrement",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Accounts");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumBid",
                table: "Items",
                type: "decimal(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BidStatus",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "BidStartDate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BidEndDate",
                table: "Items",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BidIncrementDefinitionId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccountItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentBid = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountItems", x => new { x.ItemId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_AccountItems_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BidIncrementDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidIncrement = table.Column<decimal>(type: "decimal(18,1)", nullable: false),
                    PriceRange = table.Column<decimal>(type: "decimal(18,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidIncrementDefinitions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_BidIncrementDefinitionId",
                table: "Items",
                column: "BidIncrementDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountItems_AccountId",
                table: "AccountItems",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_BidIncrementDefinitions_BidIncrementDefinitionId",
                table: "Items",
                column: "BidIncrementDefinitionId",
                principalTable: "BidIncrementDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
