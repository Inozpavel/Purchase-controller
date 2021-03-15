using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stores.Api.Migrations
{
    public partial class AddedPurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "CustomCategories",
                table => new
                {
                    CustomCategoryId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>("integer", nullable: false),
                    CustomCategoryName = table.Column<string>("text", nullable: false),
                    Description = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_CustomCategories", x => x.CustomCategoryId); });

            migrationBuilder.CreateTable(
                "PaymentMethods",
                table => new
                {
                    PaymentMethodId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MethodName = table.Column<string>("text", nullable: false),
                    MethodDescription = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodId); });

            migrationBuilder.CreateTable(
                "Purchases",
                table => new
                {
                    PurchaseId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>("integer", nullable: false),
                    TimeOfPurchase = table.Column<DateTime>("timestamp without time zone", nullable: false),
                    PaymentMethodId = table.Column<int>("integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                    table.ForeignKey(
                        "FK_Purchases_PaymentMethods_PaymentMethodId",
                        x => x.PaymentMethodId,
                        "PaymentMethods",
                        "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "ProductsReceiptInformation",
                table => new
                {
                    ProductReceiptInformationId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PurchaseId = table.Column<int>("integer", nullable: false),
                    ProductId = table.Column<int>("integer", nullable: false),
                    Count = table.Column<int>("integer", nullable: false),
                    CustomCategoryId = table.Column<int>("integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsReceiptInformation", x => x.ProductReceiptInformationId);
                    table.ForeignKey(
                        "FK_ProductsReceiptInformation_CustomCategories_CustomCategoryId",
                        x => x.CustomCategoryId,
                        "CustomCategories",
                        "CustomCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_ProductsReceiptInformation_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ProductsReceiptInformation_Purchases_PurchaseId",
                        x => x.PurchaseId,
                        "Purchases",
                        "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "CustomCategoriesForProducts",
                table => new
                {
                    ProductReceiptInformationId = table.Column<int>("integer", nullable: false),
                    CustomCategoryId = table.Column<int>("integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCategoriesForProducts",
                        x => new {x.CustomCategoryId, x.ProductReceiptInformationId});
                    table.ForeignKey(
                        "FK_CustomCategoriesForProducts_CustomCategories_CustomCategory~",
                        x => x.CustomCategoryId,
                        "CustomCategories",
                        "CustomCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_CustomCategoriesForProducts_ProductsReceiptInformation_Prod~",
                        x => x.ProductReceiptInformationId,
                        "ProductsReceiptInformation",
                        "ProductReceiptInformationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_CustomCategoriesForProducts_ProductReceiptInformationId",
                "CustomCategoriesForProducts",
                "ProductReceiptInformationId");

            migrationBuilder.CreateIndex(
                "IX_ProductsReceiptInformation_CustomCategoryId",
                "ProductsReceiptInformation",
                "CustomCategoryId");

            migrationBuilder.CreateIndex(
                "IX_ProductsReceiptInformation_ProductId",
                "ProductsReceiptInformation",
                "ProductId");

            migrationBuilder.CreateIndex(
                "IX_ProductsReceiptInformation_PurchaseId",
                "ProductsReceiptInformation",
                "PurchaseId");

            migrationBuilder.CreateIndex(
                "IX_Purchases_PaymentMethodId",
                "Purchases",
                "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CustomCategoriesForProducts");

            migrationBuilder.DropTable(
                "ProductsReceiptInformation");

            migrationBuilder.DropTable(
                "CustomCategories");

            migrationBuilder.DropTable(
                "Purchases");

            migrationBuilder.DropTable(
                "PaymentMethods");
        }
    }
}