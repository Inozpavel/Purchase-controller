using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Purchases.Api.Migrations
{
    public partial class AddedPurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Id",
                "Users",
                "UserId");

            migrationBuilder.CreateTable(
                "Purchases",
                table => new
                {
                    PurchaseId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>("integer", nullable: false),
                    Name = table.Column<string>("text", nullable: false),
                    Date = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    Price = table.Column<decimal>("numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                    table.ForeignKey(
                        "FK_Purchases_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Purchases_UserId",
                "Purchases",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Purchases");

            migrationBuilder.RenameColumn(
                "UserId",
                "Users",
                "Id");
        }
    }
}