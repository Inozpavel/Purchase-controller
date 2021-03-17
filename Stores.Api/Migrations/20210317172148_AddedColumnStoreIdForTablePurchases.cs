using Microsoft.EntityFrameworkCore.Migrations;

namespace Stores.Api.Migrations
{
    public partial class AddedColumnStoreIdForTablePurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "StoreId",
                "Purchases",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_Purchases_StoreId",
                "Purchases",
                "StoreId");

            migrationBuilder.AddForeignKey(
                "FK_Purchases_Stores_StoreId",
                "Purchases",
                "StoreId",
                "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Purchases_Stores_StoreId",
                "Purchases");

            migrationBuilder.DropIndex(
                "IX_Purchases_StoreId",
                "Purchases");

            migrationBuilder.DropColumn(
                "StoreId",
                "Purchases");
        }
    }
}