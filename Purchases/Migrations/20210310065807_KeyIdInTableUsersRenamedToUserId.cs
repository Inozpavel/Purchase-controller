using Microsoft.EntityFrameworkCore.Migrations;

namespace Purchases.Migrations
{
    public partial class KeyIdInTableUsersRenamedToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Purchases",
                newName: "PurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "Purchases",
                newName: "Id");
        }
    }
}
