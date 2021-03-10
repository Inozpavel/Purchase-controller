using Microsoft.EntityFrameworkCore.Migrations;

namespace Purchases.Migrations
{
    public partial class KeyIdInTableUsersRenamedToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Id",
                "Users",
                "UserId");

            migrationBuilder.RenameColumn(
                "Id",
                "Purchases",
                "PurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "UserId",
                "Users",
                "Id");

            migrationBuilder.RenameColumn(
                "PurchaseId",
                "Purchases",
                "Id");
        }
    }
}