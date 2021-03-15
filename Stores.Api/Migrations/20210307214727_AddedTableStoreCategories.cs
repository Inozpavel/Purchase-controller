using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stores.Api.Migrations
{
    public partial class AddedTableStoreCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Stores",
                table => new
                {
                    StoreId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreName = table.Column<string>("text", nullable: false),
                    Address = table.Column<string>("text", nullable: false),
                    Phone = table.Column<string>("text", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Stores", x => x.StoreId); });

            migrationBuilder.CreateTable(
                "Products",
                table => new
                {
                    ProductId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreId = table.Column<int>("integer", nullable: false),
                    ProductName = table.Column<string>("text", nullable: false),
                    Description = table.Column<string>("text", nullable: true),
                    Price = table.Column<decimal>("numeric", nullable: false),
                    CountInStock = table.Column<int>("integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        "FK_Products_Stores_StoreId",
                        x => x.StoreId,
                        "Stores",
                        "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "StoreCategories",
                table => new
                {
                    StoreCategoryId = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StoreId = table.Column<int>("integer", nullable: false),
                    StoreCategoryName = table.Column<string>("text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCategories", x => x.StoreCategoryId);
                    table.ForeignKey(
                        "FK_StoreCategories_Stores_StoreId",
                        x => x.StoreId,
                        "Stores",
                        "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ProductStoreCategory",
                table => new
                {
                    CategoriesStoreCategoryId = table.Column<int>("integer", nullable: false),
                    ProductsProductId = table.Column<int>("integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStoreCategory",
                        x => new {x.CategoriesStoreCategoryId, x.ProductsProductId});
                    table.ForeignKey(
                        "FK_ProductStoreCategory_Products_ProductsProductId",
                        x => x.ProductsProductId,
                        "Products",
                        "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ProductStoreCategory_StoreCategories_CategoriesStoreCategor~",
                        x => x.CategoriesStoreCategoryId,
                        "StoreCategories",
                        "StoreCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Products_StoreId",
                "Products",
                "StoreId");

            migrationBuilder.CreateIndex(
                "IX_ProductStoreCategory_ProductsProductId",
                "ProductStoreCategory",
                "ProductsProductId");

            migrationBuilder.CreateIndex(
                "IX_StoreCategories_StoreId",
                "StoreCategories",
                "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ProductStoreCategory");

            migrationBuilder.DropTable(
                "Products");

            migrationBuilder.DropTable(
                "StoreCategories");

            migrationBuilder.DropTable(
                "Stores");
        }
    }
}